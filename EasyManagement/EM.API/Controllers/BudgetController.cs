using Microsoft.AspNetCore.Mvc;
using EM.DataRepository.Budgets;
using EM.Services;
using EM.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
		private readonly IBudgetService _budgetService;
		private readonly IProjectService _projectService;

		public BudgetController(IBudgetService budgetService, IProjectService projectService)
		{
			_budgetService = budgetService;
			_projectService = projectService;
		}


		[HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
				return Ok(await _budgetService.ListBudgetsTask());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}
		}

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _budgetService.InfoBudgetTask(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(id);
            }
        }

        // POST api/<BudgetController>
        [HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBudget budget)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				await _budgetService.CreateBudgetTask(budget);
				
                if (budget.IdProject is null)
                    return Created(string.Empty, budget);

                await _projectService.ChangeStateTask(budget.IdProject, 7);

                await _projectService.UpdateDateTask(budget.IdProject);

                return Created(string.Empty, budget);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Conflict("Budget Id conflict -" + budget.IdProject); //
			}

		}

		// PUT api/<BudgetController>/5
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] InfoBudget budget)
		{
			try
			{
				await _budgetService.EditBudgetTask(budget);
				return Ok(budget);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(budget.IdProject);
			}
		}

		// DELETE api/<BudgetController>/5
		/*[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _budgetService.DisableBudgetTask(id);

				return Ok();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(id);
			}
		}  */     
       
	}
}
