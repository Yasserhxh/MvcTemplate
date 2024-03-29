﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Winnovative.HtmlToPdfClient;
//using Winnovative;

namespace Web.Tools
{
    public static class ConvertHTmlToPdf
    {
        public static async Task<ActionResult> ConvertCurrentPageToPdf(Controller pController, object Model, String pViewName1, String pFileName)
        {
            // Get the view HTML string
            string htmlToConvert = (await RenderViewAsync(pController, pViewName1, Model)).ToString();

            // Get the base URL
            string baseUrl = "https://trephoapp.azurewebsites.net";

            string PDFURL = "http://anothertest.westeurope.cloudapp.azure.com";
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter(true, PDFURL);

            //HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
            htmlToPdfConverter.LicenseKey = "b+Hx4PHg8vH29eDx9u7w4PPx7vHy7vn5+fng8A==";
            htmlToPdfConverter.ServicePassword = "";
            //htmlToPdfConverter.RenderedHtmlElementSelector = "#ToGenerate";
            //htmlToPdfConverter.HtmlViewerZoom = 130;

            htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            htmlToPdfConverter.PdfDocumentOptions.AutoSizePdfPage = false;
            //htmlToPdfConverter.PdfDocumentOptions.StretchToFit = true;
            //htmlToPdfConverter.NavigationTimeout = 480;
            //htmlToPdfConverter.PdfDocumentOptions.JpegCompressionEnabled = false;
            //htmlToPdfConverter.PdfDocumentOptions.ShowFooter = htmlToPdfConverter.PdfDocumentOptions.ShowHeader = false;
            htmlToPdfConverter.PdfDocumentOptions.RightMargin = htmlToPdfConverter.PdfDocumentOptions.LeftMargin = 10;
            htmlToPdfConverter.PdfDocumentOptions.TopMargin = 20;
            htmlToPdfConverter.PdfDocumentOptions.BottomMargin = 20;
            //htmlToPdfConverter.ConversionDelay = 2;

            htmlToPdfConverter.PdfSecurityOptions.CanPrint = true;

            //htmlToPdfConverter.PdfSecurityOptions.KeySize = EncryptionKeySize.EncryptKey128Bit;

            // Convert the HTML string to a PDF document in a memory buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlToConvert, baseUrl);


            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = pFileName + ".pdf",

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            //Response.Headers.Add("Content-Disposition", "inline; filename=" + pFileName + ".pdf");

            // Send the PDF file to browser
            //return new FileContentResult(outPdfBuffer, "application/pdf")
            //{
            //    FileDownloadName = pFileName + ".pdf"
            //};
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = pFileName + ".pdf";

            return fileResult;
        }

        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
