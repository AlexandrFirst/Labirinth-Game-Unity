using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPage : LobbyManager
{
    int min_page = 1;
    int max_page = 10;
    int current_page = 0;
    public int items_per_page = 10;
    List<string> content = new List<string>();

    public Text contentText;
    public Text pages;
    // Start is called before the first frame update
    void Start()
    {
        current_page = 1;
        contentText.text = "";
        ShowPageContent();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        if (current_page == max_page)
        {
            current_page = min_page;
        }
        else
            current_page++;

        ShowPageContent();
    }

    public void PreviousPage()
    {
        if (current_page == 1)
        {
            current_page = max_page;
        }
        else
            current_page--;

        ShowPageContent();
    }

    void ShowPageContent()
    {
        contentText.text = "";
        content = StatisticHelper.ReadFromFile();
        if (content == null)
        {
            max_page = current_page;
            contentText.text = "There are no played games";
        }
        else
        {
            max_page = content.Count / items_per_page;
            int temp = content.Count % items_per_page;
            if (temp != 0)
                max_page++;

            pages.text = string.Format("{0}/{1}", current_page, max_page);

            int up_bound = current_page * items_per_page;
            if (up_bound >= content.Count)
            {
                up_bound = content.Count;
            }

            for (int i = (current_page - 1) * items_per_page; i < up_bound; i++)
            {
                contentText.text+= content[i]+'\n';
            }
        }
        //Console.Write(current_page);
    }
}
