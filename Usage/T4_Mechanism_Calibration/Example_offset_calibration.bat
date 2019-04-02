REM positions in Calib_file are computed considering the following offsets for the 3 rotating joints of the mechanism: 
REM [0.1745 , 0.3491 , 0.5236] radians
"../../x64/Release/T4_Mechanism_Calibration.exe" "3dof_robot_uncalibrated.xml" "Calib_file.xml" "-N:3dof_robot_calibrated.xml"