<?php 
require_once("functions.php");
require_once("MySQL.php");
require_once("config.php");

$db	= new MySQL($dbServer, $dbUser, $dbPass, $dbDatabase );
$email = $_POST['email'] ;

if(!$db->Exists("SELECT * FROM subscribers WHERE email = '" . $email . "'"))
 {
$stmt = "INSERT INTO subscribers (email) VALUES ('$email')";
$db->Execute($stmt);
}
header( 'Location: http://www.mygoldensoft.com/thankyou.html' ) ;


?>
