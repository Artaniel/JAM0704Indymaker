using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public MovesManager movesManager;
    private int Tscore = 0;
    private int Bscore = 0;
    private int Mscore = 0;
    private int Iscore = 0;
    public ScoreUI scoreUI;
    private const int synergyAmp = 4;
    public ParticleSystem linkParticle;
    private Dictionary<GraphLink, ParticleSystem> particleGreenMap = new Dictionary<GraphLink, ParticleSystem>();
    private Dictionary<GraphLink, ParticleSystem> particleRedMap = new Dictionary<GraphLink, ParticleSystem>();

    private void Start()
    {
        movesManager.OnLevelStarted+= Count;
        Count();
    }

    public void Count() {
        Tscore = 0;
        Bscore = 0;
        Mscore = 0;
        Iscore = 0;
        foreach (GraphNode node in movesManager.graph.GetComponent<GraphField>().allNodes) {
            if (node.currentRobot)
            {
                Robot robot = node.currentRobot.GetComponent<Robot>();
                Tscore += robot.TSkill;
                Bscore += robot.BSkill;
                Mscore += robot.MSkill;
                Iscore += robot.ISkill;
            }
            foreach (GraphNode neirbor in node.neighbours) {                

                int synergy = SynergyMultiplier(node, neirbor);
                if (synergy > 0)
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.green;
                    link.line.endColor = Color.green;
                    
                    if (particleRedMap.ContainsKey(link))
                    {
                        var particle = particleRedMap[link];
                        Destroy(particle.gameObject);
                        particleRedMap.Remove(link);
                    }
                    
                    if (!particleGreenMap.ContainsKey(link))
                    {
                        var particles = SetGreenParticles(node, neirbor);
                        particleGreenMap.Add(link, particles);
                    }
                }
                else if (synergy < 0)
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.red;
                    link.line.endColor = Color.red;

                    if (particleGreenMap.ContainsKey(link))
                    {
                        var particle = particleGreenMap[link];
                        Destroy(particle.gameObject);
                        particleGreenMap.Remove(link);
                    }
                    
                    if (!particleRedMap.ContainsKey(link))
                    {
                        var particles = SetRedParticles(node, neirbor);
                        particleRedMap.Add(link, particles);
                    }
                }
                else
                {
                    GraphLink link = FindLink(movesManager.graph.GetComponent<GraphField>(), node, neirbor);
                    link.line.startColor = Color.white;
                    link.line.endColor = Color.white;
                    
                    if (particleGreenMap.ContainsKey(link))
                    {
                        var particle = particleGreenMap[link];
                        Destroy(particle.gameObject);
                        particleGreenMap.Remove(link);
                    }
                    
                    if (particleRedMap.ContainsKey(link))
                    {
                        var particle = particleRedMap[link];
                        Destroy(particle.gameObject);
                        particleRedMap.Remove(link);
                    }
                    
                    
                }
            }
        }
        GraphField graph = movesManager.graph.GetComponent<GraphField>();
        scoreUI.RenderScore(Tscore, Bscore, Mscore, Iscore, graph.Tgoal, graph.Bgoal, graph.Mgoal, graph.Igoal);
        ReadyCheck();
    }

    private void ReadyCheck() {
        GraphField graph = movesManager.graph.GetComponent<GraphField>();
        if (Tscore >= graph.Tgoal && Bscore >= graph.Bgoal && Mscore >= graph.Mgoal && Iscore >= graph.Igoal)
            movesManager.readyButton.interactable = true;
        else
            movesManager.readyButton.interactable = false;
    }

    private int SynergyMultiplier(GraphNode node, GraphNode neirbor) {
        if (!node.currentRobot || !neirbor.currentRobot)
            return 0;
        Robotype first = node.currentRobot.GetComponent<Robot>().robotype;
        Robotype second = neirbor.currentRobot.GetComponent<Robot>().robotype;
        if (first == Robotype.R)
        {
            if (second == Robotype.G)
            {
                Tscore -= synergyAmp;
                Mscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.B)
            {
                Tscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.Y)
            {
                Bscore += synergyAmp;
                return 1;
            }
        }
        else if (first == Robotype.G)
        {
            if (second == Robotype.R)
            {
                Tscore -= synergyAmp;
                Mscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.B)
            {
                Iscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.Y)
            {
                Mscore += synergyAmp;
                return 1;
            }
        }
        else if (first == Robotype.B)
        {
            if (second == Robotype.Y)
            {
                Iscore -= synergyAmp;
                Bscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.G)
            {
                Iscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.R)
            {
                Tscore += synergyAmp;
                return 1;
            }         
        }
        else if (first == Robotype.Y)
        {
            if (second == Robotype.B)
            {
                Iscore -= synergyAmp;
                Bscore -= synergyAmp;
                return -1;
            }
            else if (second == Robotype.G)
            {
                Mscore += synergyAmp;
                return 1;
            }
            else if (second == Robotype.R)
            {
                Bscore += synergyAmp;
                return 1;
            }
        }
        return 0;
    }

    private GraphLink FindLink(GraphField graph, GraphNode node, GraphNode neirbor) {
        foreach (GraphLink link in graph.allLinks)
        {            
            if ((link.startNode == node && link.endNode == neirbor) || (link.startNode == neirbor && link.endNode == node))
                return link;
        }
        Debug.LogWarning("did not found link");
        return null;
    }


    private ParticleSystem SetGreenParticles(GraphNode firstNode, GraphNode secondNode)
    {
        Vector3 dir;
        ParticleSystem particle;
        linkParticle.startColor = Color.green;
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }

        return null;
    }

    
    private ParticleSystem SetRedParticles(GraphNode firstNode, GraphNode secondNode)
    {
        Vector3 dir;
        ParticleSystem particle;
        linkParticle.startColor = Color.red;
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.G &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.R)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }
        
        if (firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B && 
            secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y)
        {
            dir = secondNode.transform.position - firstNode.transform.position;   
            return particle = Instantiate(linkParticle, firstNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
        }

        if (secondNode.currentRobot.GetComponent<Robot>().robotype == Robotype.B &&
            firstNode.currentRobot.GetComponent<Robot>().robotype == Robotype.Y)
        {
            dir = firstNode.transform.position - secondNode.transform.position;   
            return particle = Instantiate(linkParticle, secondNode.transform.position, 
                quaternion.LookRotation(dir, Vector3.up));
            
        }

        return null;
    }
    public void ClearParticlesMap()
    {
        foreach (var pair in particleGreenMap)
        {
            Destroy(pair.Value.gameObject);
        }
        particleGreenMap.Clear();
        
        foreach (var pair in particleRedMap)
        {
            Destroy(pair.Value.gameObject);
        }
        particleRedMap.Clear();
    }

    private void OnDestroy()
    {
        movesManager.OnLevelStarted -= Count;
    }
}