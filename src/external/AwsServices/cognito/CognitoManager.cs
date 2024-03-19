using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CloudGateway;

namespace AwsServices;

public class CognitoManager : IAuthGateway
{
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
    public CognitoManager()
    {
        var awsAccessId = SecretManager.GetSecret("aws_access_id").GetAwaiter().GetResult();
        var awsAccessKey = SecretManager.GetSecret("aws_access_key").GetAwaiter().GetResult();
        _cognitoClient = new AmazonCognitoIdentityProviderClient(
                                    awsAccessId,
                                    awsAccessKey,
                                    Amazon.RegionEndpoint.USEast1);
    }

    public async Task<string> CadastrarUsuario(string username, string password)
    {
        var userSignUpRequest = new SignUpRequest
        {
            ClientId = await SecretManager.GetSecret("cognito_configuration"),
            Password = password, 
            Username = username
        };
        
        var usuario = await  _cognitoClient.SignUpAsync(userSignUpRequest);
        return usuario.UserSub;
    }

}
