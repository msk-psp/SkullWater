/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PUBLIC_MEMBER_VARIABLES

        public bool stopFlag = false;
        public bool isStarted = false;
        
        #endregion //PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        private myDistance[] firstSphereDistance;
        private myDistance[] firstLineDistance;

        private Vector3 firstLineScale;
        private Vector3 firstSphereScale;

        private Camera myCamera;
        private RaycastHit hit, hit2;

        private GameObject[] Spheres;
        private GameObject[] Lines;


        private const string SPHERE_TAG_NAME = "Sphere";
        private const string LINE_TAG_NAME = "Lines";
        private const int SPHERE_NUMBER = 8;


        private string SCENENAME;
        private string BURNITURE = "Burniture";
        private string LAYOUT = "Layout";
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            SCENENAME = SceneManager.GetActiveScene().name;
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            Spheres = new GameObject[SPHERE_NUMBER];
            Lines = new GameObject[GameObject.FindGameObjectsWithTag(LINE_TAG_NAME).Length];
            Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);

            for (int i = 0; i < SPHERE_NUMBER; i++)
            {
                Spheres[i] = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (i + 1));
            }

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS
        void Update()
        {
            if (isStarted && (SCENENAME == BURNITURE || SCENENAME == LAYOUT) && !stopFlag)
            {
                float rate;
                for (int i = 0; i < firstSphereDistance.Length; i++)
                {
                    Physics.Raycast(myCamera.ScreenPointToRay(Spheres[i].transform.position), out hit2);
                    rate = hit2.distance / firstSphereDistance[i].GetDistance();
                    if (rate > 1.0f) rate *= 0.5f;
                    Spheres[i].transform.localScale = firstSphereScale * rate;
                }
                for (int i = 0; i < firstLineDistance.Length; i++)
                {
                    Physics.Raycast(myCamera.ScreenPointToRay(Lines[i].transform.position), out hit2);
                    rate = hit2.distance / firstLineDistance[i].GetDistance();
                    if (rate > 1.0f) rate *= 0.5f;
                    Lines[i].transform.localScale = firstLineScale * rate;
                }
            }
        }


        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
                turnOnUpdate();
            }
            else
            {
                OnTrackingLost();
                turnOffUpdate();
            }
        }

        public void ChangeObjects()
        {
            const string CLONE = "(Clone)";
            Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);
            for (int i = 0; i < SPHERE_NUMBER; i++)
            {
                Spheres[i] = GameObject.Find(SPHERE_TAG_NAME + (i + 1) + CLONE);
            }
            firstSphereScale = new Vector3(15.0f, 15.0f, 15.0f);
            firstLineScale = new Vector3(2f, 2f, 2f);
        }

        public void turnOffUpdate()
        {
            stopFlag = false;
            isStarted = false;
        }

        public void turnOnUpdate()
        {
            stopFlag = true;
            isStarted = true;
        }
        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            /*if (GameObject.Find("GenerateRoom") != null)
            {
                GameObject.Find("GenerateRoom").GetComponent<RawImage>().color = Color.red;
            }*/

            if (ButtonMotion.State != 2 && ButtonMotion.State != 3)
            {
                ButtonMotion.State = 2;
                ButtonMotion.ChangeState = 0;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            /*if (!isStarted && (SCENENAME == BURNITURE))
            {
                SetDistances();
            }
            else if (!isStarted && (SCENENAME == LAYOUT))
            {
                SetDistances();
            }*/
        }
        private void SetDistances()
        {
            hit = new RaycastHit();
            hit2 = new RaycastHit();
            firstSphereDistance = new myDistance[8];
            firstLineDistance = new myDistance[12];
            firstSphereScale = new Vector3(0.4f, 0.4f, 0.4f);
            firstLineScale = new Vector3(0.02f, 0.02f, 0.02f);
            myCamera = Camera.main;

            for (int i = 0; i < firstSphereDistance.Length; i++)
            {
                firstSphereDistance[i] = new myDistance();
                Physics.Raycast(myCamera.ScreenPointToRay(Spheres[i].transform.position), out hit);
                firstSphereDistance[i].SetDistance(hit.distance);
            }
            for (int i = 0; i < firstLineDistance.Length; i++)
            {
                firstLineDistance[i] = new myDistance();
                Physics.Raycast(myCamera.ScreenPointToRay(Lines[i].transform.position), out hit);
                firstLineDistance[i].SetDistance(hit.distance);
            }
            isStarted = true;
        }

        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS

        class myDistance
        {
            public bool isSet;
            private float distance;

            public myDistance() { isSet = false; }
            public void SetDistance(float value)
            {
                distance = value;
                isSet = true;
            }
            public float GetDistance() { return distance; }
        }
    }
}