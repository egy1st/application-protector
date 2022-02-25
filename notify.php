<?php
require_once("global.php");
require_once("encrypt.php");
require_once("MySQL.php");

$db= new MySQL($dbServer, $dbUser, $dbPass, $dbDatabaseName);
$cur_date = date("Y-m-d") ;
$cur_time = date("H:i:s") ;

$para1 = $_GET['para1'] ;
$para2 = $_GET['para2'] ;
$para3 = $_GET['para3'] ;
$para4 = $_GET['para4'] ;
$para5 = $_GET['para5'] ;

       $stmt = "INSERT INTO notifications (companyid, productid, version, type, reseller, initdate, inittime) VALUES ('$para1', '$para2' , '$para3', '$para5', '$para4', '$cur_date', '$cur_time' )";
       $db->Execute($stmt);
       echo "NOMACHINEPRINT";
	   ?>
