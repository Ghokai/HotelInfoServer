﻿# Hotelify:) - Server

    This applications validates, visualizes, uploads and converts to various
    formats of given csv files which are consists of hotel informations like
    name, address, rating, contact, phone, uri

    You can find sample input files in sampleInputFiles folder

## About This Application

    It is just a job interview programming assigment which is coded by Gökay
    Arpacı (gokayarpaci@gmail.com)

### Built With

    Asp.Net Core 2.0    

### Installing
    
    clone the repository
    dotnet restore 
    dotnet run
    
    or

    you can open project with visual studio and press f5 :)

### As a detail of this app

   this application developed with extensible architecture
   it takes one type input file, 
   validates it's data with spesific validation rules
   and generates to specific output formats.

   we can easily extend and support with
   new input formats, new validation rules, new output formats
   and manage this input types,validation rules and output formats from
   configuration (see appsettings.json) without changing single line of code.
  
