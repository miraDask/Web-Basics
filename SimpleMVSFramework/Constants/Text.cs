namespace SimpleMVSFramework.Constants
{

    class Text
    {
        internal const string NewLine = "\r\n";
        internal const string Response = "HTTP/1.0 200 OK" + NewLine +
                                  "Server: SoftUniServer/1.0" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  // "Location: https://google.com" + NewLine +
                                  // "Content-Disposition: attachment; filename=niki.html" + NewLine +
                                  "Content-Lenght: ";
        internal const string Request = @"<form action='/Account/Login' method='post'>
<input type=date name='date' />
<input type=text name='username' />
<input type=password name='pasword' />
<input type=submit value='Login' />
</form>";

    }
}
