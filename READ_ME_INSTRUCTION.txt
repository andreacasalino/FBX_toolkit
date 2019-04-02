All tools are contained in x64/Release (or Debug): they are all executables that can be ran from shell
passing some arguments as input. 
Call any .exe with the /? option for the helper:

Example:
open a prompt from the folder ./x64/Release and launch:
T1_XML_2_FBX.exe /?

Available tools:
-T1_XML_2_FBX.exe: 
 Converts a kinematic hierarchy with shapes into an .fbx file (preserving the hierarchy).
 All information must be contained into an .xml file, see the examples provided. 
 You can add to Unity the produced .fbx, associating the script with name Pose_Manager.cs, 
 for controlling the mechanism. 

-T2_Inverse_Kinematics.exe:
 Computes the kinematic inversion of a kinematic tree. Such a tree is expressed by an .xml 
 file with the format described above. The position of the end effectors (they can be multiple
 when considering a kinematic tree and not a chain) are expressed in a .txt file, see the examples
 provided for the formats.

-T3_Frame_Calibration.exe:
 Extracts the location (rotation + position) of a frame in the space.
 Usefull for computing some data required by the .xml describing the kinematic tree.
 Put a calibrator.stl object in the same position of the frame of interest  and then save that .stl
 with respect to the world. The corner of the calibrator is the only vertex having a cone of facets that are
 all mutually orthogonal. The position of such a corner must be the same of the origin of the frame to calibrate;
 while the x axis must contain the longest edge connected to the corner; the y axis must contain the medium size one 
 and finally the z axis must contain the shortest.
 See Calibrator.png.
 It is possible to rescale the Calibrator, without varying the proportions.

-T4_Mechanism_Calibration.exe:
 For automatically calibrates a mechanism.
 It computes the offsets of the joints, in order to assume the same possesed by the real mechanism.
 A new .xml with the computed offsets is automatically recomputed. 
 See the examples provided.


All samples are contained in the folder Usage, as .bat files.
