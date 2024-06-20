using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AppContext = Context.Context.AppContext;

namespace Services.Utils;

public class ExecuteScripts
{
    private readonly AppContext _context;
    private readonly ILogger<ExecuteScripts> _logger;
    
    public ExecuteScripts(AppContext dbContext, ILogger<ExecuteScripts> logger)
    {
        _context = dbContext;
        _logger = logger;
    }
    
    public void ExecuteSQLFiles(string scriptDirectory)
    {
        if (Directory.Exists(scriptDirectory))
        {
            var scriptFiles = Directory.GetFiles(scriptDirectory, "*.sql").OrderBy(f => f);
            foreach (var file in scriptFiles)
            {
                var scriptName = Path.GetFileName(file);
                
                if (IsScriptExecuted(scriptName))
                {
                    continue;
                }

                try
                {
                    var script = File.ReadAllText(file);
                    _context.Database.ExecuteSqlRaw(script);
                    LogScriptExecution(scriptName);
                    _logger.LogInformation($"Executed script: {scriptName}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error executing script {scriptName}: {ex.Message}");
                }
            }
        }
        else
        {
            _logger.LogWarning($"Directory not found: {scriptDirectory}");
        }
    }
    
    private bool IsScriptExecuted(string scriptName)
    {
        return _context.ScriptLogs.Any(log => log.ScriptName == scriptName);
    }

    private void LogScriptExecution(string scriptName)
    {
        _context.ScriptLogs.Add(new ScriptLog() { Id = Guid.NewGuid() , ScriptName = scriptName });
        _context.SaveChanges();
    }
}