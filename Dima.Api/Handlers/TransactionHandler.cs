using System.Transactions;
using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Transaction = Dima.Core.Models.Transaction;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            
            };
        
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        
            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao criar a transaçao. request: {@request}", request);
            return new Response<Transaction?>(null, 500, "Não foi possivel criar sua transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(s => s.Id == request.Id && s.UserId == request.UserId);

            if (transaction == null )
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
            
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao atualizar a transaçao. request: {@request}", request);
            return new Response<Transaction?>(null, 500, "Não foi possivel atualizar sua transação");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(s => s.Id == request.Id && s.UserId == request.UserId);
            
            if (transaction == null )
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
            
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao deletar a transaçao. request: {@request}", request);
            return new Response<Transaction?>(null, 500, "Não foi possivel deletar sua transação");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(s => s.Id == request.Id && s.UserId == request.UserId);
            
            return transaction == null 
                ? new Response<Transaction?>(null, 404, "Transação não encontrada") 
                : new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao obter a transaçao. request: {@request}", request);
            return new Response<Transaction?>(null, 500, "Não foi possivel obter sua transação");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao determinar a data de inicio ou termino. request: {@request}", request);
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel determinar a data de inicio ou termino");
        }

        try
        {
            var query = context.Transactions.AsNoTracking()
                .Where(s =>
                    s.CreatedAt >= request.StartDate &&
                    s.CreatedAt <= request.EndDate &&
                    s.UserId == request.UserId)
                .OrderBy(s => s.CreatedAt);
        
            var transaction = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
        
            return new PagedResponse<List<Transaction>?>(transaction, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            Log.Error(e, "Erro ao obter as transaçoes. request: {@request}", request);
            return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel obter suas transações");
        }
    }
}