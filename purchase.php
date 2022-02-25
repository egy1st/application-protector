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

$para5  = $_GET['para5'] ;
$para5  = trim($para5) ;

$para6  = $_GET['para6'] ;
$para6  = trim($para6) ;

$para7  = $_GET['para7'] ;
$para7  = trim($para7) ;

$para8  = $_GET['para8'] ;
$para8  = trim($para8) ;

$KeyAlgorithm2 = $_GET['para9'] ;
$KeyAlgorithm3 = $_GET['para10'] ;

$para11  = $_GET['para11'] ;


$result = CheckValidity ($para5) ;

if ($result == ture) {


    $stmt = "SELECT * FROM purchase WHERE fingerprint = '" . $para4 . "' and  serial = '" .$para5 .  "'" ;
	if(!$db->Exists($stmt))

	 {
	   $stmt = "INSERT INTO purchase (companyid, productid, version,  fingerprint, serial, name, email, machineprint, reseller, initdate, inittime) VALUES ('$para1', '$para2' , '$para3', '$para4',  '$para5', '$para6', '$para7', '$para8', '$para11', '$cur_date', '$cur_time' )";
       $db->Execute($stmt);
       echo "VALID";
       
   	 }

     else 
       {

       $stmt = "SELECT * FROM purchase WHERE fingerprint = '" . $para4 . "' and  serial = '" .$para5 .  "'" ;
       $res = mysql_query($stmt) ;
       $query = mysql_fetch_array($res);
       $machineprint = $query["machineprint"] ;

       if ($machineprint == $para8)
          {
           $stmt = "UPDATE purchase set tries = tries +1, email = '" . $para7 . "'  WHERE fingerprint = '" . $para4 . "' and  serial = '" .$para5 .  "'";
	       $db->Execute($stmt);
	       echo "VALID";
          }
       else echo "NOMATCH";

 	   } // exist
     } // valid
       else echo "INVALID" ;


function  CheckSum($strNum)
{

$blnDoubleFlag = 0;

$X =  strlen($strNum) - 1 ;

        for ($X;  $X > -1 ; $X--)
           {
            
            $intDigit = ord(substr($strNum, $X, 1)) ;

            if ($intDigit >= 48 && $intDigit <= 57) // is it a digit starting 0 and ending 9
            {
                    $intDigit = $intDigit - 48 ;

                    if ($blnDoubleFlag == 1 )
                    {
                       $intDigit = $intDigit + $intDigit ;
                        if ($intDigit > 9)
                          {
                            $intDigit = $intDigit - 9 ;
                          }
                     }
                        
                    if ($blnDoubleFlag == 0) $blnDoubleFlag = 1  ;
                    else if ($blnDoubleFlag == 1) $blnDoubleFlag = 0  ;

                    $intCheckSum = $intCheckSum + $intDigit ;
                    if ($intCheckSum > 9)
                      {
                        $intCheckSum = $intCheckSum - 10 ;
                      }
                    
              } // end if
        } // for
        
		if ($intCheckSum == 0)  return true ;
        else return false ;
    }
    
    
	function Formula($Num, $Mode)
	  {
            
            global $KeyAlgorithm2 ;
            global $KeyAlgorithm3 ;
            
			$result = (13 * pow($Num,3)) + (12 * pow($Num, 2)) + ( $KeyAlgorithm2 * pow($Num,1)) + ($KeyAlgorithm3 * pow($Num,0)) ;
			
					
            $result = strval($result) ;
						
            if      ($Mode == 1) return substr($result, 0, 3);
            Else If ($Mode == 2) return substr($result, 3, 3);
            Else If ($Mode == 3) return substr($result, 6, 3);
            Else If ($Mode == 4) return substr($result, 9, 3);

      }
		
		
		function CheckValidity($MyKey)
		{
		
		    global $KeyAlgorithm1 ;
		
            $KeyRnd = substr($MyKey, 0, 4) ;
            $KeyVer = ZeroPad(strval( $KeyAlgorithm1 * $KeyAlgorithm1), 8) ;
		    $KeyRnd -= intval(substr($KeyVer, 0, 4));

            if ((CheckSum(substr($MyKey, 0, 4) . substr($MyKey, 5, 3) . substr($MyKey, 9, 3) . substr($MyKey, 13, 3) . substr($MyKey, 17, 4))) == false ) return  false ;
            if (substr($MyKey, 5, 3)  != (Formula($KeyRnd, 1) + intval(substr($KeyVer, 4, 1)))) return false  ;
            if (substr($MyKey, 9, 3)  != (Formula($KeyRnd, 2) + intval(substr($KeyVer, 5, 1)))) return false ;
            if (substr($MyKey, 13, 3) != (Formula($KeyRnd, 3) + intval(substr($KeyVer, 6, 1)))) return false ;
            if (substr($MyKey, 17, 3) != (Formula($KeyRnd, 4) + intval(substr($KeyVer, 7, 1)))) return false ;

            return true ;
        }
            

        function ZeroPad($str_String, $int_Count) 
        {

           if ($str_String != "") return  str_repeat("0", $int_Count - strlen(trim($str_String))) . trim($str_String) ;

        }
		
    ?>
    
