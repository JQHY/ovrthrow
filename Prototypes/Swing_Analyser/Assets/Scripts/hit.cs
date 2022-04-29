using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class hit : MonoBehaviour
{

  // remember that target radius can be considered 3x bigger as you only have to touch circumference

    ArrayList path_list = new ArrayList();
    ArrayList required_path = new ArrayList()
    {
      "high2",
      "high1",
      "high1",
      "high2",
      "high3",
      "high4",
      "high5"
    };

    int total = 7; // equals the size of required_path
    int current = 0;

    Vector3 startingPos;

    // Stopwatch stopwatch = new Stopwatch();

    public Text throw_prompt;
    public int remove_time;

    // used for making prompts dissapear
    IEnumerator RemoveText(Text throw_prompt)
    {
        yield return new WaitForSeconds(remove_time);
        throw_prompt.text = "";
        // could implement fade
    }

    void Start()
    {
      throw_prompt.text = "";

      startingPos = this.transform.position;
      // in-game, a dissolve effect would be nice. Either snap back to starting position or another "reload" method.
    }

    // void TriggerHoldDown

    void OnTriggerEnter(Collider other)
    {
        // colliders are hit in the order: 2,1,2,3,4,5
        // using the order of collisions, you could also alert the user of how they are throwing wrong.
        // always alert user when resetting throw
        // e.g. "bad throw! You went back on yourself", and "rethrow !"

        // throw cycle already completed

        if (current == total)
        {
          return;
        }

        else if (other.gameObject.tag == (string) required_path[current])
        {

          current += 1;
        }

        // throw is wrong
        // (note: 'current' doesn't increment on a wrong throw)
        else
        {
          if (current == 0 & other.gameObject.tag == "high3")
          {
            print("you need to swing behind you first!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing behind you first!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          if (current == 1 & other.gameObject.tag == "high2")
          {
            print("you need to swing higher backwards!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing higher backwards!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          if (current == 1 & other.gameObject.tag == "high3")
          {
            print("you need to swing higher backwards!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing higher backwards!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          if (current == 2 & other.gameObject.tag == "high2")
          {
            print("you need to swing higher backwards!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing higher backwards!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          if (current == 5 & other.gameObject.tag == "high3")
          {
            print("you need to swing further forwards!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing further forwards!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          if (current == 6 & other.gameObject.tag == "high4")
          {
            print("you need to swing further forwards!");
            current = 0;
            this.transform.position = startingPos;
            throw_prompt.text = "You need to swing further forwards!";
            StartCoroutine(RemoveText(throw_prompt));
          }

          print("bad throw! Try again");
          current = 0;
          this.transform.position = startingPos;
          throw_prompt.text = "Bad throw! Try again";
          StartCoroutine(RemoveText(throw_prompt));
          // cannonball should dissolve and be reloaded.
      }
    }

    void OnTriggerExit(Collider other)
    {
      if (current == total && other.gameObject.tag == "release")
      {
        // if Input.GetButtenDown() == OFF

        print("successful throw!");
        throw_prompt.text = "Good Swing!";
        StartCoroutine(RemoveText(throw_prompt));

        // fade off?
      }
    }

// implement snapping cannonball back to starting position after current=0 lines.

}
