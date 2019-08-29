using BusinessLogicLayer.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Yevhenii_KoliesnikTask1.HttpHandler
{
    public class CustomUploadHandler: IHttpAsyncHandler
    {
          private readonly IGameService _gameService;

        public CustomUploadHandler(IGameService gameServices)           
        {
            _gameService = gameServices;
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            throw new NotImplementedException();
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public async void ProcessRequest(HttpContext context)
        {
            HttpPostedFile filedata = context.Request.Files[0];
            var gameKey = context.Request["gameKey"];
            await UploadPictureAsync(filedata, gameKey.ToString());
        }
        


     
        public async Task<bool> UploadPictureAsync(HttpPostedFile Filedata, string gameKey)
        {

            var game = _gameService.GetByKey(gameKey, null);
            var fileName = game.Key + "." + Path.GetExtension(Filedata.FileName);
            var res = await UploadAsync(fileName, Filedata);

            if (res)
            {
                game.Picture = fileName;
                _gameService.Update(game);
              
            }
            return res;
           
        }


        private async Task<bool> UploadAsync(string fileName, HttpPostedFile file)
        {
            return Upload(fileName, file);
        }

        private bool Upload(string fileName, HttpPostedFile file)
        {
            var res = false;
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine("~/Content/Images/gamepicture", fileName);
                file.SaveAs(path);
                res = true;
            }
            return res;
        }
    }
}
