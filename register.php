<?php

require_once("global.php");
require_once("MySQL.php");
require_once("encrypt.php");

$para1 = $_GET['para1'] ;
$para2 = $_GET['para2'] ;
$para3 = $_GET['para3'] ;
$para4 = $_GET['para4'] ;
$para5 = $_GET['para5'] ;
$para6 = $_GET['para6'] ;
$para7 = $_GET['para7'] ;
$para8 = $_GET['para8'] ;
$para9 = $_GET['para9'] ;
$para10 = $_GET['para10'] ;

$para13 = $_GET['para13'] ;
$para14 = $_GET['para14'] ;
$para15 = $_GET['para15'] ;
$para16 = $_GET['para16'] ;
$para17 = $_GET['para17'] ;
$para18 = $_GET['para18'] ;
$para19 = $_GET['para19'] ;
$para20 = $_GET['para20'] ;

$db= new MySQL($dbServer, $dbUser, $dbPass, $dbDatabaseName);
$cur_date = date("Y/m/d") ;
$cur_time = date("H:i:s") ;
	   
   if(!$db->Exists("SELECT * FROM registration WHERE fingerprint = '" . $para4 . "' AND machineprint = '" . $para5 . "'" ))
	 {
	   $stmt = "INSERT INTO registration (companyid, productid, version,  fingerprint, machineprint, reseller, username, email, usercompany, country, initdate, inittime, PROCESSOR_IDENTIFIER, NUMBER_OF_PROCESSORS, PROCESSOR_ARCHITECTURE, OS, VisualStudioDir, LOGONSERVER, COMPU_USERNAME, COMPUTERNAME) VALUES ('$para1', '$para2' , '$para3', '$para4', '$para5', '$para6', '$para7','$para8','$para9','$para10','$cur_date', '$cur_time', '$para13', '$para14', '$para15', '$para16', '$para17', '$para18', '$para19', '$para20' )";
       echo $stmt;
	   $db->Execute($stmt);
	 }  

     else
       {
       $stmt = "UPDATE registration set tries = tries +1 WHERE fingerprint = '" . $para4 . "' AND machineprint = '" . $para5 . "'";
	   $db->Execute($stmt);
	   echo "FOUND" ; 
	   }

       
        
       
       
       
	   

?>
