using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaomi.Legacy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kaomi.WebAPI.Legacy.Controllers
{
    [Route("Kaomi")]
    [ApiController]
    public class KaomiController : ControllerBase
    {
        // GET Kaomi
        [HttpGet]
        public ActionResult<string> Get()
        {
            var result = JsonConvert.SerializeObject(new
            {
                status = "Kaomi.WebAPI appears to be working."
            });
            return result;
        }

        // GET Kaomi/PullFromUri
        [HttpGet("PullFromUri")]
        public ActionResult<string> PullFromUri(string fileName, string uri)
        {
            try
            {
                KaomiLoader.PullFromUri(fileName, new Uri(uri));
                var result = JsonConvert.SerializeObject(new
                {
                    status = $"File {fileName} pulled from requested URI."
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error pulling assembly: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/Load
        [HttpGet("Load")]
        public ActionResult<string> Load(string path)
        {
            try
            {
                var result = JsonConvert.SerializeObject(new
                {
                    asmId = KaomiLoader.Load(path)
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error loading assembly: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/ListAssemblies
        [HttpGet("ListAssemblies")]
        public ActionResult<string> ListAssemblies()
        {
            try
            {
                var asms = KaomiLoader.List().ToArray();
                var result = JsonConvert.SerializeObject(new
                {
                    count = asms.Length,
                    assemblies = asms
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error listing loaded assemblies: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/Unload
        [HttpGet("Unload")]
        public ActionResult<string> Unload(string path)
        {
            try
            {
                KaomiLoader.Unload(path);
                var result = JsonConvert.SerializeObject(new
                {
                    status = $"Assembly {path} removed from memory."
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error unloading assembly: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/InstanceProcess
        [HttpGet("InstanceProcess")]
        public ActionResult<string> InstanceProcess(string id, string type)
        {
            try
            {
                KaomiLoader.InstanceProcess(id, type);
                var result = JsonConvert.SerializeObject(new
                {
                    status = "Process executed; Output should be visible on the Kestrel console window."
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error instancing process: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/ListProcesses
        [HttpGet("ListProcesses")]
        public ActionResult<string> ListProcesses(string id, string type)
        {
            try
            {
                var procs = KaomiLoader.ListProcesses().ToArray();
                var result = JsonConvert.SerializeObject(new
                {
                    count = procs.Length,
                    processes = procs
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error listing processes: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/HasResults
        [HttpGet("HasResults")]
        public ActionResult<string> HasResults(string process)
        {
            try
            {
                var result = JsonConvert.SerializeObject(new
                {
                    process,
                    hasResults = KaomiLoader.HasResults(process)
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error checking for process result: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/GetResults
        [HttpGet("GetResults")]
        public ActionResult<string> GetResults(string process)
        {
            try
            {
                var results = KaomiLoader.GetResults(process).ToArray();
                var result = JsonConvert.SerializeObject(new
                {
                    process,
                    count = results.Length,
                    results
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error returning process results: {e.Message}"
                });
                return error;
            }
        }

        // GET Kaomi/SendMessage
        [HttpGet("SendMessage")]
        public ActionResult<string> SendMessage(string process, string message)
        {
            try
            {
                KaomiLoader.SendMessage(process, message);
                var result = JsonConvert.SerializeObject(new
                {
                    status = "Message sent successfully."
                });
                return result;
            }
            catch (Exception e)
            {
                var error = JsonConvert.SerializeObject(new
                {
                    error = $"Error sending message: {e.Message}"
                });
                return error;
            }
        }
    }
}
