const ApiController = (function(){

    const clientid = '';
    const clientSecret='';

    //private dmethode
    const getToken = async () =>
    {
        const result = await fetch('', {

            method: 'Post',
            headers:
            {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Basic' + btoa(clientid + ':' + clientSecret)
            },
            body: 'grant_type=Client_credentials'
        });
        const data = await result.json();
        return data.access_token;
   }
})();