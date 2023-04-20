using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    public Transform Target1;
    public Transform Target2;
    public Transform Target4;
    public static bool mover = false;
    public float speedMultiplier = 0.1f;
    public bool hit;

    public override void OnEpisodeBegin()
    {
        // reset de positie en orientatie als de agent gevallen is
        if (this.transform.localPosition.y < 0.45)
        {
            this.transform.localPosition = new Vector3(-4, 0.5f, 0); this.transform.localRotation = Quaternion.identity;
        }
        if (-4.01 < this.transform.localPosition.x && this.transform.localPosition.x < -3.99)
        {
            this.transform.localPosition = new Vector3(-4, 0.5f, 0); this.transform.localRotation = Quaternion.identity;
        }
        if (-0.01 < this.transform.localPosition.z && this.transform.localPosition.z < 0.01)
        {
            this.transform.localPosition = new Vector3(-4, 0.5f, 0); this.transform.localRotation = Quaternion.identity;
        }

        // verplaats de target naar een nieuwe willekeurige locatie 
        float random = Random.value;

        if (random > 0.5)
        {
            hit = false;
        }
        else
        {
            hit = true;
        }
        if (hit)
        {
            Target1.localPosition = new Vector3(10, 0.5f, 0);
            mover = true;
            move.newrandom();
        }
        else
        {
            Target2.localPosition = new Vector3(10, 0.5f, 0);
            mover = true;
            move.newrandom();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent posities
        sensor.AddObservation(Target1.localPosition);
        sensor.AddObservation(Target2.localPosition);
        sensor.AddObservation(this.transform.localPosition);

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float distanceToTargethit = Vector3.Distance(this.transform.localPosition, Target1.localPosition);
        float distanceToTargetavoid = Vector3.Distance(this.transform.localPosition, Target2.localPosition);
        float distanceTogroundagent = Vector3.Distance(this.transform.localPosition, Target4.localPosition);

        // Acties, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.y = actionBuffers.ContinuousActions[0] * speedMultiplier;
        if (distanceTogroundagent < 4.04)
        {
            //transform.Translate(controlSignal * speedMultiplier);
            transform.GetComponent<Rigidbody>().AddForce(controlSignal, ForceMode.Impulse);

        }

        // Beloningen
        if (hit)
        {
            if (distanceToTargethit < 1.42f)
            {
                //Debug.Log(GetCumulativeReward());
                AddReward(5.0f);
                mover = false;
                Target1.localPosition = new Vector3(5, -1, 1);
                move.newrandom();
                Debug.Log(GetCumulativeReward());
                EndEpisode();
            }
            if (Target1.localPosition.x < -8f)
            {
                //Debug.Log(GetCumulativeReward());
                SetReward(-2.0f);
                mover = false;
                Target1.localPosition = new Vector3(5, -1, 1);
                move.newrandom();
                Debug.Log(GetCumulativeReward());
                EndEpisode();

            }
        }
        else if (!hit)
        {
            if (distanceToTargetavoid < 1.42f)
            {
                //Debug.Log(GetCumulativeReward());
                SetReward(-2.0f);
                mover = false;
                Target2.localPosition = new Vector3(7, -1, 1);
                move.newrandom();
                Debug.Log(GetCumulativeReward());
                EndEpisode();

            }
            if (Target2.localPosition.x < -8f)
            {
                //Debug.Log(GetCumulativeReward());
                AddReward(5.0f);
                mover = false;
                Target2.localPosition = new Vector3(7, -1, 1);
                move.newrandom();
                Debug.Log(GetCumulativeReward());
                EndEpisode();
            }
        }
        if (distanceTogroundagent < 4.04)
        {
            //Debug.Log("ja");
            AddReward(0.01f);
        }

    }

    /*
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float distanceTogroundagent = Vector3.Distance(this.transform.localPosition, Target4.localPosition);
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (distanceTogroundagent < 4.04){
            continuousActionsOut[0] = Input.GetAxis("Jump");
        }
    }
    */
}
