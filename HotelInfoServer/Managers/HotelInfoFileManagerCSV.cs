using System;
using System.Collections.Generic;
using HotelInfoServer.Models;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using HotelInfoServer.ModelValidators;
using HotelInfoServer.AssemblyLoaders;

namespace HotelInfoServer.Managers
{
    public class HotelInfoFileManagerCSV : HotelInfoFileManagerBase //, IHotelInfoFileManager
    {
        //extract address with this regex pattern because address consists comma characters and quotes
        //example=  "63847 Lowe Knoll, East Maxine, WA 97030-4876"
        private const string cnstAddrRegExpPattern = "\"([^\"]*)\"";

        public HotelInfoFileManagerCSV(IHostingEnvironment env, IDynamicLoader dynamicLoader, IGenericModelValidator<HotelInfo> validator)
            : base(env, dynamicLoader, validator) { }


        //we can simply add new FileManagers which supports different formats(different than CSV) 
        //by overriding "ParseHotelInfoToList" and implementing format specific logic
        public override HotelInfo[] ParseHotelInfoToList(IFormCollection form)
        {
            //check file is exists
            this.ValidateForm(form);

            IFormFile inputFile = form.Files[0];
            List<HotelInfo> hotelInfo = new List<HotelInfo>();

            //read file
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var headers = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var adrr = "";
                    //extract address in to variable
                    Match match = Regex.Match(line, cnstAddrRegExpPattern);
                    if (match.Success)
                    {
                        adrr = match.Groups[1].Value;
                    }
                    //remove address parts from line
                    line = Regex.Replace(line, cnstAddrRegExpPattern, "");
                    //split line by comma  into vaues
                    var values = line.Split(',');
                    if (values.Length == 6)
                    {
                        //initialize model
                        var item = new HotelInfo(values[0], adrr, Int32.Parse(values[2]), values[3], values[4], values[5]);
                        //validate model by validation rules which loading by appsettings.json
                        this.ValidateModel(item);
                        //add to list
                        hotelInfo.Add(item);
                    }
                    else
                    {
                        throw new Exception("CSV Format Error!");
                    }
                }
            }
            //get sort field and sort direction from request object
            OutputProperties outProps = MapFormCollectionToOutputProperties(form);
            HotelInfo[] hotelInfoArray = hotelInfo.ToArray();
            //sort model list according to output properties
            hotelInfoArray = this.SortHotelInfos(hotelInfoArray, outProps);

            return hotelInfoArray;
        }
    }
}
