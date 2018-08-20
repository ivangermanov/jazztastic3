using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {

    public void loadBank()
    {
        SceneManager.LoadScene("LocalBank", LoadSceneMode.Single);
    }
    public void loadTicket()
    {
        SceneManager.LoadScene("Ticket", LoadSceneMode.Single);
    }
    public void loadFeedback()
    {
        SceneManager.LoadScene("Feedback", LoadSceneMode.Single);
    }
    public void loadLoanedItems()
    {
        SceneManager.LoadScene("LoanedItems", LoadSceneMode.Single);
    }

}
