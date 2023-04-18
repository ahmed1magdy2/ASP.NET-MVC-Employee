using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.Domain;

namespace WebApplication2.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly MVCContext mvccontext;


		public EmployeeController(MVCContext mvccontext)
		{
			this.mvccontext = mvccontext;
		}

		public MVCContext Mvccontext { get; }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var employees = await mvccontext.Employees.ToListAsync();
			return View(employees);
		}

		[HttpGet]
		public async Task<IActionResult> view(Guid id)
		{
			var employee = await mvccontext.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (employee != null)
			{
				var viewModel = new UpdateEmployeeViewModel()
				{
					Id = employee.Id,
					Name = employee.Name,
					Email = employee.Email,
					Salary = employee.Salary,
					DateOfBirth = employee.DateOfBirth,
					Department = employee.Department
				};
				return View(viewModel);
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> view(UpdateEmployeeViewModel model)
		{
			var employee = await mvccontext.Employees.FindAsync(model.Id);
			if (employee != null)
			{
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Salary = model.Salary;
				employee.DateOfBirth = model.DateOfBirth;
				employee.Department = model.Department;

				await mvccontext.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
		{
			var employee = await mvccontext.Employees.FindAsync(model.Id);
			if (employee != null)
			{
				mvccontext.Employees.Remove(employee);
				await mvccontext.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
		{
			var employee = new Employee()
			{
				Id = Guid.NewGuid(),
				Name = addEmployeeRequest.Name,
				Email = addEmployeeRequest.Email,
				Salary = addEmployeeRequest.Salary,
				DateOfBirth = addEmployeeRequest.DateOfBirth,
				Department = addEmployeeRequest.Department
			};

			await mvccontext.Employees.AddAsync(employee);
			await mvccontext.SaveChangesAsync();

			return RedirectToAction("Add");
		}
	}

}
