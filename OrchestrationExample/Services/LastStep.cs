using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace OrchestrationExample.Services
{
    public class LastStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Microservice B");
            return ExecutionResult.Next();
        }
    }
}