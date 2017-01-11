using UnityEngine;
using System.Collections;


/***************
* This class handles user key inputs, instantiate and destroy the organisms.
* 
* User presses Up/Down key to switch primary; Single Cell, Multi Cell, Fusion Cell, Elemental Cell, Armored Cell.
* Left/Right key to switch primitive in current organism.
**************/

public class TestOrganismsScript : MonoBehaviour {

	#region Variables

	// Elements
	public GameObject[] m_PrefabListSingle;
	public GameObject[] m_PrefabListMulti;
	public GameObject[] m_PrefabListFusion;
	public GameObject[] m_PrefabListElemental;
	public GameObject[] m_PrefabListArmored;


	// Index of current element
	int m_CurrentOrganismIndex = -1;

	// Index of current particle
	int m_CurrentPrimitiveIndex = -1;	

	// Name of current element
	string m_OrganismName = "";	

	// Name of current particle
	string m_PrimitiveName = "";

	// Current particle list
	GameObject[] m_CurrentOrganismList = null;

	// GameObject of current particle that is showing in the scene
	GameObject m_CurrentPrimitive = null;

	#endregion

	// ######################################################################
	// MonoBehaviour Functions
	// ######################################################################

	#region Component Segments

	// Use this for initialization
	void Start () {

		// Check if there is any particle in prefab list
		if(m_PrefabListSingle.Length>0 ||
			m_PrefabListMulti.Length>0 ||
			m_PrefabListElemental.Length>0 ||
			m_PrefabListArmored.Length>0 ||
			m_PrefabListFusion.Length>0)
		{
			// reset indices of element and particle
			m_CurrentOrganismIndex = 0;
			m_CurrentPrimitiveIndex = 0;

			// Show particle
			ShowOrganism();
		}
	}

	// Update is called once per frame
	void Update () {

		// Check if there is any particle in prefab list
		if(m_CurrentOrganismIndex!=-1 && m_CurrentPrimitiveIndex!=-1)
		{
			// User released Up arrow key
			if(Input.GetKeyUp(KeyCode.UpArrow))
			{
				m_CurrentOrganismIndex++;
				m_CurrentPrimitiveIndex = 0;
				ShowOrganism();
			}
			// User released Down arrow key
			else if(Input.GetKeyUp(KeyCode.DownArrow))
			{
				m_CurrentOrganismIndex--;
				m_CurrentPrimitiveIndex = 0;
				ShowOrganism();
			}
			// User released Left arrow key
			else if(Input.GetKeyUp(KeyCode.LeftArrow))
			{
				m_CurrentPrimitiveIndex--;
				ShowOrganism();
			}
			// User released Right arrow key
			else if(Input.GetKeyUp(KeyCode.RightArrow))
			{
				m_CurrentPrimitiveIndex++;
				ShowOrganism();
			}
		}
	}

	// OnGUI is called for rendering and handling GUI events.
	void OnGUI () {

		// Show version number
		GUI.Window(1, new Rect((Screen.width-260), 5, 250, 80), InfoWindow, "Info");

		// Show Help GUI window
		GUI.Window(2, new Rect((Screen.width-260), Screen.height-85, 250, 80), ParticleInformationWindow, "Help");
	}

	#endregion Component Segments

	// ######################################################################
	// Functions Functions
	// ######################################################################

	#region Functions

	// Remove old Particle and do Create new Particle GameObject
	void ShowOrganism()
	{
		// Make m_CurrentElementIndex be rounded
		if(m_CurrentOrganismIndex>7)
		{
			m_CurrentOrganismIndex = 0;
		}
		else if(m_CurrentOrganismIndex<0)
		{
			m_CurrentOrganismIndex = 7;
		}

		// Update current m_CurrentElementList and m_ElementName
		if(m_CurrentOrganismIndex==0)
		{
			m_CurrentOrganismList = m_PrefabListSingle;
			m_OrganismName = "Single Cell";
		}
		else if(m_CurrentOrganismIndex==1)
		{
			m_CurrentOrganismList = m_PrefabListMulti;
			m_OrganismName = "Multi Cell";
		}
		else if(m_CurrentOrganismIndex==2)
		{
			m_CurrentOrganismList = m_PrefabListFusion;
			m_OrganismName = "Fusion Cell";
		}
		else if(m_CurrentOrganismIndex==3)
		{
			m_CurrentOrganismList = m_PrefabListElemental;
			m_OrganismName = "Elemental Cell";
		}
		else if(m_CurrentOrganismIndex==4)
		{
			m_CurrentOrganismList = m_PrefabListArmored;
			m_OrganismName = "Armored Cell";
		}


		// Make m_CurrentParticleIndex be rounded
		if(m_CurrentPrimitiveIndex>=m_CurrentOrganismList.Length)
		{
			m_CurrentPrimitiveIndex = 0;
		}
		else if(m_CurrentPrimitiveIndex<0)
		{
			m_CurrentPrimitiveIndex = m_CurrentOrganismList.Length-1;
		}

		// update current m_ParticleName
		m_PrimitiveName = m_CurrentOrganismList[m_CurrentPrimitiveIndex].name;

		// Remove Old particle
		if(m_CurrentPrimitive!=null)
		{
			DestroyObject(m_CurrentPrimitive);
		}

		// Create new particle
		m_CurrentPrimitive = (GameObject) Instantiate(m_CurrentOrganismList[m_CurrentPrimitiveIndex]);
	}

	// Show Help window
	void ParticleInformationWindow(int id)
	{
		GUI.Label(new Rect(12, 25, 280, 20), "Up/Down: "+m_OrganismName+" ("+(m_CurrentPrimitiveIndex+1) + "/" + m_CurrentOrganismList.Length +")");
		GUI.Label(new Rect(12, 50, 280, 20), "Left/Right: "+ m_PrimitiveName.ToUpper());
	}

	// Show Info window
	void InfoWindow(int id)
	{
		GUI.Label(new Rect(15, 25, 240, 20), "Organism 0.0.1");
	}

	#endregion {Functions}
}
