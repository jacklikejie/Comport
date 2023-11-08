namespace Comport.DataBase
{
    #region MES上传
    enum UploadResult
    {
        不上传 = 1,
        上传成功 = 2,
        上传失败 = 3,
        离线数据 = 4,
    }

    enum ValidateResult
    {
        调试料 = 1,
        校验成功 = 2,
        校验失败 = 3,
        离线数据 = 4,
    }
    #endregion
}