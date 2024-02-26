using EtamConverter.Data;
using EtamConverter.Methods;
using EtamConverter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;


namespace EtamConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Index()
        {

            var connectionString = Configuration["ConnectionStrings:EtamConverterDB"];
            EtamConverterMethods db = new EtamConverterMethods();

            db.InitDBConnection(connectionString);


            FilePathModel model = new FilePathModel();


           return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Test Submit



        [HttpPost]
        public IActionResult Upload(FilePathModel model)
        {



            if (ModelState.IsValid)
            {
                bool AggConvert = false;
                if (model.SignalType != null)
                {
                    string SingalType = model.SignalType.ToString();

                    if (SingalType == "agg")
                    {
                        //:: TODO :: UserDefaults?
                        AggConvert = true;

                    } else
                    {
                        //:: TODO :: UserDefaults?
                    }

                }

                // check first if filename is empty
              //  if (model.File == null)
               // {
               //     //:: TODO Redirect user to Error ::
               // } 

                // getting ready
                string WOInputFileName = model.File.FileName;


                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WOTo_Arianna_Data/");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // :: TODO :::

                string FilePrefix = "29120";

                // ariana FileName
                string EtamOutPutFileName = Path.GetFileNameWithoutExtension(Path.GetFileName(WOInputFileName)) + "-etam.sch";
                //ArianaOutputFileName += "-arianna.txt";

                // upload file
                string WOInputFilePath = Path.Combine(path, FilePrefix + "-" + Path.GetFileName(WOInputFileName));

    
                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string orignalPath = fileInfo.ToString();


                // check if file is uploaded

                //string fileNameWithPath = Path.Combine(path, WOInputFileName);
                bool FileUploaded = false;

                try
                {

                    using (var stream = new FileStream(WOInputFilePath, FileMode.Create))
                    {
                        model.File.CopyTo(stream);
                        stream.Close();
                        FileUploaded = true;

                    }
                } catch
                {
                    FileUploaded = false;
                }

                if (FileUploaded == true)  {

                    /// Do Something Overhere
                    string EtamOutPutFilePath = Path.Combine(path, FilePrefix + EtamOutPutFileName);

                    // do the convert function

                    EtamConverterMethods converter = new EtamConverterMethods();
                    var connectionString = Configuration["ConnectionStrings:EtamConverterDB"];
                    bool result = converter.ConvertToEtam(WOInputFilePath, EtamOutPutFilePath, connectionString, AggConvert);



                    if (result)
                   {

                        System.IO.File.Delete(WOInputFilePath);

                        // load/buffer our output file ...
                        byte[] fileBytes = System.IO.File.ReadAllBytes(EtamOutPutFilePath);


                        // Erase our output file, unless 'debug' is on.. NYI:
                        System.IO.File.Delete(EtamOutPutFilePath);

                        // Download our output file
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, EtamOutPutFileName);
                    }
                } else
                {
                    ViewBag.Message = "Select a file name ...";
                }
 
            }
            return View("Index", model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}