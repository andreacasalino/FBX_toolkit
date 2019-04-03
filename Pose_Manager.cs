using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose_Manager : MonoBehaviour {
    
    class Link_frame {
        public Link_frame(Transform trf)
        {
            pRef_to_Frame = trf;
            mQ = 0;

            Vector3 local_orientation = trf.localEulerAngles;
            mTeta_offset = local_orientation.z;
            mAlfa_offset = local_orientation.x;
        }
        
        public float Pose {
            get
            {
                return mQ;
            }
            set
            {
                pRef_to_Frame.localEulerAngles = new Vector3(mAlfa_offset, 0, -(mTeta_offset + value));
                mQ = value;
            }
        }

     // data
        private Transform pRef_to_Frame;
        private float mQ;
        private float mTeta_offset;
        private float mAlfa_offset;
    }
    private Link_frame[] mLinks;

    private int Extract_joint_pos(string body_name) { //return -1 in case the name of the considered body is not the one of a joint
        int pos = -1;

        int cursor = 0;
        int skipped_underline = 0;
        for (int k=0; k<body_name.Length; k++) {
            if(body_name[k] == '_')
            {
                skipped_underline++;
                if(skipped_underline == 2)
                {
                    cursor = k + 1;
                    break;
                }
            }
        }

        if (body_name.Length >= (cursor + 3) )
        {
            if ((body_name[cursor] == 'J') && (body_name[cursor + 2] == '_'))
            {
                cursor = cursor + 3;
                int cursor_end = 0;
                for (int k = cursor; k < body_name.Length; k++)
                {
                    if (body_name[k] == '_')
                    {
                        cursor_end = k - 1;
                        break;
                    }
                }

                string word_to_convert = new string(body_name.ToCharArray(), cursor, cursor_end - cursor + 1);
                pos = System.Convert.ToInt32(word_to_convert);
                pos--;
            }
        }

        return pos;
    }
    private class Link_info_extraction {
        public Link_info_extraction(int pos, Transform frame) { pos_Q = pos; Frame = frame; }

        public int pos_Q;
        public Transform Frame;
    }
    private void Init_ref(Transform obj) {

        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>();
        List<Link_info_extraction> links_unordered = new List<Link_info_extraction>();
        foreach (Transform G_obj in children)
        {
            int joint_pos = Extract_joint_pos(G_obj.name);
            Debug.Log(joint_pos);
            if (joint_pos >= 0)
            {
                links_unordered.Add(new Link_info_extraction(joint_pos, G_obj));
            }
        };

        mLinks = new Link_frame[links_unordered.Count];
        foreach (Link_info_extraction extrct_el in links_unordered)
        {
            mLinks[extrct_el.pos_Q] = new Link_frame(extrct_el.Frame);
        };

    }

	// Use this for initialization
	void Start () {
        Init_ref(this.gameObject.transform);
	}

    public float[] Pose_Q
    {
        get
        {
            float[] q = new float[mLinks.Length];
            int k = 0;
            foreach (Link_frame l in mLinks)
            {
                q[k] = l.Pose;
                k++;
            }
            return q;
        }
        set
        {
            int k = 0;
            foreach (Link_frame l in mLinks)
            {
                l.Pose = value[k];
                k++;
            }
            k = 0;
        }
    }

    public int DOF
    {
        get
        {
            return mLinks.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //dummy increment of pose
        float[] qq = this.Pose_Q;

        for (int k = 0; k < mLinks.Length; k++)
        {
            qq[k] += 1.0f;
        }
        this.Pose_Q = qq;
    } 
}
