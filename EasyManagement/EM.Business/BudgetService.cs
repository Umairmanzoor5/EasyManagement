using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Budgets;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<InfoBudget>> ListBudgetsTask()
        {                       
            //var listBudget = await _context.Budgets.Select(budget => new InfoBudget
            //{
            //    Description = budget.Description,
            //    //IdClient = budget.,
            //    IdState = budget.IdState,
            //    CreateDate = budget.CreateDate,
            //    UpdateDate = budget.UpdateDate,
            //    Value = budget.Value,
            //})
            //.ToListAsync();

            return null;            
        }

        public async Task<InfoBudget> InfoBudgetTask(int budgetId)
        {
            var budget = await _context.Budgets.SingleAsync(x => x.Id == budgetId);

            var infoBudget = new InfoBudget
            {
                Description = budget.Description,
                //IdClient = budget.IdClient,
                //IdState = budget.IdState,
                CreateDate = budget.CreateDate,
                UpdateDate = budget.UpdateDate,
                //Value = budget.Value,
            };

            return infoBudget;
        }
        
        public async Task CreateBudgetTask(CreateBudget budget)
        {
            float? total = 0;
            foreach (var product in budget.Products)
            {
                var t = (product.Price * product.Quantity);

                var a = (t * (product.Discount / 100));

                a ??= 0;

                total += t - a;
            }

            budget.Total = total;

            var newBudget = new Budget
            {
                Description = budget.Description,
                IdProject = budget.IdProject,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IdState = 1,
                Value = (decimal)budget.Total
             };

            await _context.Budgets.AddAsync(newBudget);
            await _context.SaveChangesAsync();

            foreach (var newProd in budget.Products.Select(product => new ProductsBudget
                     {
                         Quantity = product.Quantity,
                         IdBudjet = newBudget.Id,
                         Price = (decimal)product.Price,
                         Discount = product.Discount,
                         IdProduct = product.Reference
                     }))
            {
                await _context.ProductsBudgets.AddAsync(newProd);
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditBudgetTask(InfoBudget budget)
        {
            //var result = await _context.Budgets.SingleAsync(x => x.Id == budget.Id);
            //result.Description = budget.Description;
            ////result.IdClient = budget.IdClient;
            //result.IdState = budget.IdState;
            //result.CreateDate = budget.CreateDate;
            //result.UpdateDate = budget.UpdateDate;
            //result.Value = budget.Value;

            await _context.SaveChangesAsync();
        }
        
       /* public async Task DisableBudgetTask(int budgetId)
        {
            var result = await _context.Budgets.SingleAsync(x => x.Id == budgetId);

            result.Active = false;

            await _context.SaveChangesAsync();
        }
       */
    }
}
