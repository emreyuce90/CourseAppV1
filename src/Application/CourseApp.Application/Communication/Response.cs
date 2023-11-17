using Newtonsoft.Json;

namespace CourseApp.Application.Communication {
    /// <summary>
    /// API işlemlerinin geri dönüş mesajıdır. Başarılı yada başarısız durumları için ilgili alanlara bakın.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Response {

        /// <summary>
        /// İşlem başarılı ise <code>true</code> döner.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İşlem başarısız ise başarısız olma nedenini verir.
        /// </summary>
        public string Message { get; set; }

        /// <summary>s
        /// İşlemlerin başarısız olma nedeni için gerekli olabilecek açıklamaları içerir.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// İşlem başarılı ise 0, başarısız ise ilgili hata kodunu verir. Daha önce tanımlanmamış bir hata ise yine 0 verir.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Hata bilgisinin teknik detaylarını verir. Hatanın nedenini bilmiyorsanız, sorunun çözümü için bu detayları sistem yöneticisine iletin.
        /// </summary>
        public List<string> Details { get; set; }

        /// <summary>
        /// İşlem başarılı ise isteninlen veriyi bu alandan edinin. 
        /// </summary>
        public object Resource { get; set; }

        private int _level = 0;
        private Exception _exception;

        public Response() {
            Success = true;
        }

        public Response(string error) {
            Success = false;
            Message = error;
        }

        public Response(Exception ex) {

            if (_exception != null)
                throw new Exception("Exception already added.");

            _exception = ex;

            Success = ex == null;
            AddMessages(ex);
        }

        public Response(object data) {
            Success = data != null;
            Resource = data;
            Message = data == null ? "Data is empty" : null;
        }

        private void AddMessages(Exception ex) {

            var p = AppDomain.CurrentDomain.BaseDirectory;

            if (string.IsNullOrWhiteSpace(Message))
                Message = ex.Message.Replace(p, "/");

            if (_level > 0) {
                Details ??= new List<string>();
                Details.Add(_level + ".Message: " + ex.Message.Replace(p, "/"));
                Details.Add(_level + ".Type   : " + ex.GetType().Name);
                if (!string.IsNullOrWhiteSpace(ex.StackTrace))
                    Details.Add(_level + ".Stack  : " + ex.StackTrace?.Replace(p, "/"));
            }

            _level++;

            if (ex.InnerException != null) {
                AddMessages(ex.InnerException);
            }
        }

        public Exception GetException() {
            if (_exception != null)
                return _exception;
            _exception = Success
                ? null
                : new Exception($"{Message ?? "Unknown error message."} {Comment}", new Exception(string.Join(Environment.NewLine, Details ?? new List<string>())));
            return _exception;
        }

        public void Error(Exception ex, string comment = null) {
            _exception = ex;
            Success = ex == null;
            Comment = comment;
            AddMessages(ex);
            if (Comment != null)
                if (Message?.Contains(Comment) == true)
                    Comment = null;
        }
    }


    /// <summary>
    /// PI işlemlerinin geri dönüş mesajıdır. Başarılı yada başarısız durumları için ilgili alanlara bakın.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Response<T> : Response {
        public new T Resource { get; set; }

        public Response() {
            Success = true;
        }

        public Response(T resource) {
            Success = true;
            Resource = resource;
        }
        public Response(string error) {
            Success = false;
            Message = error;
            Resource = default;
        }
        public Response(Response response) {
            if (response != null) {
                Success = response.Success;
                Message = response.Message;
                Comment = response.Comment;
                if (Comment != null)
                    if (Message?.Contains(Comment) == true)
                        Comment = null;
                Details = response.Details;
                Code = response.Code;
                Resource = response.Resource == null
                    ? default
                    : response.Resource?.GetType() == typeof(T) ? (T)Convert.ChangeType(response.Resource, typeof(T)) : default;
            }
        }

        public Response(Response<T> response) {
            if (response != null) {
                Success = response.Success;
                Message = response.Message;
                Comment = response.Comment;
                if (Comment != null)
                    if (Message?.Contains(Comment) == true)
                        Comment = null;
                Details = response.Details;
                Code = response.Code;
                Resource = response.Resource == null
                    ? default
                    : (T)Convert.ChangeType(response.Resource, typeof(T));
            }
        }

        public Response(Exception ex) : base(ex) { }

    }
}
