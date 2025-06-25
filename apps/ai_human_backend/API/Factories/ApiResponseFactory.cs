using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace API.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext Context)
        {
            var Errors = Context.ModelState.Where(predicate: M => M.Value.Errors.Any())
            .Select(selector: M => new ValidationError()
            {
                Field = M.Key,
                Errors = M.Value.Errors.Select(selector: E => E.ErrorMessage)
            });
            var Response = new ValidationErrorToReturn()
            {
                ValidationErros = Errors
            };
            return new BadRequestObjectResult(error: Response);
        }
    }
}
