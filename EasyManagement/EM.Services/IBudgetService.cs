using EM.DataRepository.Budgets;

namespace EM.Services;

public interface IBudgetService
{
    Task<List<InfoBudget>> ListBudgetsTask();
    Task<InfoBudget> InfoBudgetTask(int budgetId);
    Task CreateBudgetTask(CreateBudget budget);
    Task EditBudgetTask(InfoBudget budget);
    //Task DisableBudgetTask(int budgetId);
}