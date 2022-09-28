using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pipeman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI responseText;
    [SerializeField] private TMP_InputField bodyText;
    [SerializeField] private TMP_InputField endpointText;
    [SerializeField] private TMP_Dropdown endpointType;
    [SerializeField] private ScrollRect scrollRect;
    
    private void Start()
    {
        //string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam augue arcu, ultricies sit amet libero vitae, aliquam consectetur ante. Integer sed metus enim. Curabitur eget libero semper, lobortis velit ut, porta nulla. Nunc hendrerit velit nisl, a blandit turpis suscipit vitae. Aenean mollis interdum dui. Nullam id eleifend magna. Phasellus mattis laoreet dolor ac dapibus. Mauris tincidunt erat augue, et aliquet risus ornare semper. Suspendisse auctor, lorem et commodo blandit, arcu risus convallis dolor, sed fermentum nulla mauris vitae eros. Etiam commodo lobortis turpis eget pellentesque. Praesent ultricies vehicula sapien lobortis mattis. Proin imperdiet nulla sit amet luctus pulvinar. Pellentesque nisl diam, tincidunt et semper sed, feugiat eget sem. Sed a lectus a orci egestas tristique.Lorem ipsum dolor sit amet, consectetur adipiscing elit.Donec accumsan ipsum ac arcu rutrum dignissim.Sed ut odio hendrerit, auctor neque sit amet, venenatis lectus. Donec congue, est viverra accumsan varius, nisi sem aliquam ipsum, in commodo leo ipsum eget sem.Nulla nec malesuada enim, porttitor bibendum elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam at tincidunt erat. Sed tincidunt mauris nibh. Nullam non dolor consequat, suscipit ante vel, bibendum tellus.Nullam at consequat arcu, nec laoreet dolor. Morbi ut dui odio. Nam urna est, bibendum nec luctus ut, facilisis vitae leo. Aenean erat nisi, viverra eu metus ut, hendrerit commodo magna. Donec nec mattis massa, vel tempus nisi. Nam posuere velit vel lectus convallis, nec posuere arcu porta. Donec elementum eros risus, vitae ultricies urna viverra semper. Phasellus bibendum ligula fringilla augue dignissim laoreet.Cras sit amet finibus elit.Proin tempus maximus lacus, a dapibus nulla aliquam id. Mauris porttitor ex sed elit consequat, ut porta diam finibus.Integer at elit arcu. Pellentesque non ex convallis, dapibus eros ac, iaculis erat.Nam lobortis ultrices nulla elementum luctus. Pellentesque ultrices mauris id.";
       // DisplayText(text);
    }

    public void DisplayText(string text)
    {
        responseText.text = text;
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void SendButton()
    {
        string type = endpointType.captionText.text;
        string endpoint = endpointText.text;
        string body = bodyText.text;

        string fullEndpoint = type[0] + type.Substring(1).ToLower() + endpoint;

        switch (fullEndpoint)
        {
            case EndpointConstants.getPlayer:
                Debug.Log(EndpointConstants.getPlayer);
                break;
            case EndpointConstants.putPlayer:
                Debug.Log(EndpointConstants.putPlayer);
                break;
        } 
    }
}
