using HotelInfoServer.OutputWriters;
using HotelInfoServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HotelInfoServer.ModelValidators;
using HotelInfoServer.AssemblyLoaders;

namespace HotelInfoServer.Managers
{
    public abstract class HotelInfoFileManagerBase : IHotelInfoFileManager, IValidationRule<HotelInfo>
    {
        private const string cnstOutputFolder = "uploads";
        private const string cnstHotelInfoValidationRulesConfigName = "HotelInfoValidationRules";
        private const string cnstOutputWriterTypesConfigName = "OutputWriterTypes";


        private IHostingEnvironment _env;
        private IDynamicLoader _dynamicLoader;
        private IGenericModelValidator<HotelInfo> _validator;

        private string _OutputFolderPath { get; set; }
        private HotelInfo[] _HotelInfos { get; set; }
        private IHotelInfoOutputWriter[] _OutputWriters { get; set; }

        //injecting dependencies
        public HotelInfoFileManagerBase(IHostingEnvironment env, IDynamicLoader dynamicLoader, IGenericModelValidator<HotelInfo> validator)
        {
            this._env = env;
            this._dynamicLoader = dynamicLoader;
            this._validator = validator;

            this._OutputFolderPath = Path.Combine(_env.ContentRootPath, cnstOutputFolder);

            //hotel info validation types are  loading from appsettings.json file at runtime
            //we can manage validation types by editing "HotelInfoValidationRules" at appsettings.json
            this._validator.LoadValidationRules(cnstHotelInfoValidationRulesConfigName, ",");
        }

        private IHotelInfoOutputWriter[] LoadDynamicallyFromConfig()
        {
            //initializes assembly types to given generic type from configuration
            return _dynamicLoader.LoadAssembliesDynamicallyFromConfig<IHotelInfoOutputWriter>(cnstOutputWriterTypesConfigName, ",");
        }

        public void GenerateOutputs(string fullOutputPath)
        {
            //output formats loading dynamically from config. it can manageable from appsettings.json
            _OutputWriters = LoadDynamicallyFromConfig();
            foreach (var outputhandler in _OutputWriters)
            {
                //write hotel info list to given output path
                outputhandler.WriteToOutputFile(fullOutputPath, _HotelInfos);
            }
        }

        //load output properties from request (sorting field and sorting direction)
        public OutputProperties MapFormCollectionToOutputProperties(IFormCollection form)
        {
            OutputProperties outProps = null;
            string SortFieldKey = "SortField";
            string SortDirectionKey = "SortDirection";

            if (form.Any())
            {
                if (form.Keys.Contains(SortFieldKey))
                {
                    outProps = new OutputProperties();

                    outProps.SortField = form[SortFieldKey];
                    if (form.Keys.Contains(SortDirectionKey))
                        outProps.SortDirection = form[SortDirectionKey];
                }
            }
            return outProps;
        }

        //sort array by given field name and field direction
        public HotelInfo[] SortHotelInfos(HotelInfo[] hotelInfoArray, OutputProperties outProps)
        {

            if (outProps != null)
            {
                PropertyInfo pi = typeof(HotelInfo).GetProperty(outProps.SortField);
                if (pi != null)
                {
                    if (outProps.SortDirection == "Desc")
                    {
                        return hotelInfoArray.OrderByDescending(item => pi.GetValue(item, null)).ToArray();
                    }
                    else
                    {
                        return hotelInfoArray.OrderBy(item => pi.GetValue(item, null)).ToArray();
                    }
                }
            }

            return hotelInfoArray;
        }

        //writes input file to upload folder with date time info
        public string WriteInputFile(IFormFile inputFile)
        {
            byte[] fileByteArray;
            string fileName = "";
            using (var stream = inputFile.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                fileByteArray = memoryStream.ToArray();
            }
            fileName = inputFile.FileName;

            DateTime now = DateTime.Now;
            var fileNamePrefix = now.ToString("ddMMyyyy_HHmmss");

            var fileFullPath = Path.Combine(_OutputFolderPath, fileNamePrefix + fileName);

            FileStream fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileByteArray, 0, fileByteArray.Length);
            fileStream.Close();

            //returns  output file name for output format files
            return getOutputFileName(fileFullPath);

        }

        private string getOutputFileName(string inputFileFullPath)
        {
            //returns output file full path without extension
            var outputFileName = Path.GetFileNameWithoutExtension(inputFileFullPath);
            var fullOutputPath = Path.Combine(_OutputFolderPath, outputFileName);
            return fullOutputPath;
        }

        //check input file exists in form object
        public void ValidateForm(IFormCollection form)
        {
            IFormFile inputFile = null;
            if (form.Files.Count > 0)
            {
                inputFile = form.Files[0];
                if (inputFile == null || inputFile.Length == 0)
                {
                    throw new Exception("File is empty!");
                }
            }
            else
            {
                throw new Exception("File Not Found!");
            }

        }
        public void ProcessFile(IFormCollection form)
        {
            //read & validate file
            this._HotelInfos = ParseHotelInfoToList(form);

            
            IFormFile inputFile = null;
            inputFile = form.Files[0];

            //writes input file server directory and returns output file path 
            string fullOutputPath = WriteInputFile(inputFile);

            //writes ouput files to output path
            GenerateOutputs(fullOutputPath);
        }

        //this method will be implemented by derived class
        //because of each input file formats will be have it's own read & parsing implementation
        public abstract HotelInfo[] ParseHotelInfoToList(IFormCollection form);

        public bool ValidateModel(HotelInfo t)
        {
            //validate model with validation rules
            return _validator.ValidateModel(t);
        }
    }
}
