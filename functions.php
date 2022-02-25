<?php 


function divideKeywords()
{
	global $db;
	global $keywordsArr;
	global $keywords_num;
	
	$oldLetter 	= "";
	$output		= "";
	
	
	echo "Preparing Keywords List ....<br>";
	flush();
	
	$y 		= 0;
	
    for ($x = 0; $x < $keywords_num; $x++)
	
    {
		$y++;
		$row		= &$keywordsArr[$x];
		$keyword	= $row["KeyWord"];
		$letter		= $keyword[0];
		$letterCode	= ord($letter);
		
		$letter		= substr($letter,0,1);
		//echo "letter" . $letter . "\t"  ;
		if($letter != $oldLetter)
			$y = 0;
			
		$output		= (int)($y / 1000000);
		$row["Letter"] 	= $letter;
		$row["Letter_Key"] 	= $output;
		
		$stm  = "UPDATE keywords SET Letter = '$letter', Letter_Key = '$output' WHERE KeyWord = '$keyword'" ;
		$db->Execute($stm);
			//echo "affected " . mysql_affected_rows() . "<br>" ;
			//echo $stm . "<br>";
	}
	
	$stm  = "UPDATE keywords SET visited=0" ;
	$db->Execute($stm);
		
    echo "Keywords have been devided ....<br><br>";
	flush();	
}

function CreateFolders()
{
    global $db;
	global $targetPath;
	global $targetPath_ads;
	global $keywordsArr;
	global $keywords_num;
	global $publisherID;
	global $main_keyword;
	global $siteName;
    global $siteName1;
	global $siteName2;
	global $keywordsbag;
	
	
	echo "Creating Folders ....<br>";
	flush();
	
	
	$letter 	= "";
	$keywords_limit	= 0;
	
	
	$arr = $db->FetchAll("SELECT k.Letter, k.Letter_Key FROM keywords AS k GROUP BY k.Letter, k.Letter_Key", false, true);
	echo "Folders are " . count($arr)."<br>";
	
	for($x = 0; $x < count($arr); $x++)
	{
		$row		= &$arr[$x];
		//$letter 	= trim($row["Letter_Key"]) != "0" ? $row["Letter"] . "_" . $row["Letter_Key"] : $row["Letter"];
		$letter 	= trim($row["Letter"]) ;

		if(!file_exists("$targetPath/$letter"))
			mkdir("$targetPath/$letter");
	}
	
	if(!file_exists("$targetPath/images"))
		mkdir("$targetPath/images");
		
	copy("link.css", "$targetPath/link.css");
	copy("images/logo.gif", "$targetPath/images/logo.gif");
	copy("images/img01.gif", "$targetPath/images/img01.gif");
	copy("images/img02.gif", "$targetPath/images/img02.gif");
	copy("images/img03.gif", "$targetPath/images/img03.gif");
	copy("images/img04.gif", "$targetPath/images/img04.gif");
	copy("images/img05.gif", "$targetPath/images/img05.gif");
	copy("images/img06.gif", "$targetPath/images/img06.gif");
	copy("images/header.jpg", "$targetPath/images/header.jpg");
	
	


  for($x = 0; $x < count($arr); $x++)
	{
		$row		= &$arr[$x];
		//$letter 	= trim($row["Letter_Key"]) != "0" ? $row["Letter"] . "_" . $row["Letter_Key"] : $row["Letter"];
		$letter 	= trim($row["Letter"]) ;

		if(!file_exists("$targetPath_ads/$letter"))
			mkdir("$targetPath_ads/$letter");
	}
	
	if(!file_exists("$targetPath_ads/images"))
		mkdir("$targetPath_ads/images");
		
	copy("link.css", "$targetPath_ads/link.css");
	copy("images/logo.gif", "$targetPath_ads/images/logo.gif");
	copy("images/img01.gif", "$targetPath_ads/images/img01.gif");
	copy("images/img02.gif", "$targetPath_ads/images/img02.gif");
	copy("images/img03.gif", "$targetPath_ads/images/img03.gif");
	copy("images/img04.gif", "$targetPath_ads/images/img04.gif");
	copy("images/img05.gif", "$targetPath_ads/images/img05.gif");
	copy("images/img06.gif", "$targetPath_ads/images/img06.gif");
	copy("images/header.jpg", "$targetPath_ads/images/header.jpg");
	
}

function putDash($oldStr)
{
	return str_replace(" ", "_", $oldStr);
}

function prepareIndexPages()
{
	global $db;
	global $targetPath;
	global $targetPath_ads;
	global $keywordsArr;
	global $keywords_num;
	global $publisherID;
	global $main_keyword;
	global $siteName;
    global $siteName1;
	global $siteName2;
	global $keywordsbag;
	global $keywordsAll_num;
	global $keywordsbag_num;
	
	echo "Preparing Index Pages ....<br>";
	flush();
	
	
	$letter 	= "";
	$keywords_limit	= 0;
	
	
	//$arr = $db->FetchAll("SELECT k.Letter, k.Letter_Key FROM keywords AS k GROUP BY k.Letter, k.Letter_Key", false, true);
	//echo "Folders are " . count($arr)."<br>";
	
	
    $newstr2 	    = "";
	$template	    = preg_split('/\$\$(\w+)\$\$/', file_get_contents("prepare_index_.tpl"), -1 , PREG_SPLIT_DELIM_CAPTURE );
	$template_ads	= preg_split('/\$\$(\w+)\$\$/', file_get_contents("prepare_index_ads.tpl"), -1 , PREG_SPLIT_DELIM_CAPTURE );
	
	
	
    for($x = 0; $x < $keywords_num; $x++)
	{
		$row		= &$keywordsArr[$x];
		$keyword	= $row["KeyWord"];
		//$letter 	= trim($row["Letter_Key"]) != "0" ? $row["Letter"] . "_" . $row["Letter_Key"] : $row["Letter"];
        $letter 	= trim($row["Letter"]);
		//$letterCode	= ord($letter);
		//$letter		= ($letterCode >= 65 && $letterCode <= 90) || ($letterCode >= 97 && $letterCode <= 122) ? strtoupper($letter) : "X";
		
		$newstr2 	.= "<p><a href=\"$letter/" .  putDash($keyword) . ".htm\">$keyword</a></p>\r\n";
		
		if(($x+1) % 100 == 0 || ($x+1) == $keywordsAll_num)
		{
			$fileName 		= "$targetPath/" . ($x+1) . ".htm";
			$fileName_ads 		= "$targetPath_ads/" . ($x+1) . ".htm";
			$tpl			= $template;
			$tpl_ads			= $template_ads;
			
			$values 		= array();
			$values["main_keyword"]	= $main_keyword;
			$values["Keyword"]	= $keyword;
			$values["publisher_id"]	= $publisherID;
			$values["newstr2"]	= $newstr2;
			$values["site_name"]	= $siteName;
            $values["site_name1"]	= $siteName1;
			$values["site_name2"]	= $siteName2;
			
			$newstr2 		= "";
			
			fillTemplate($tpl, $values);
			fillTemplate($tpl_ads, $values);
			file_put_contents($fileName, join("", $tpl));
			file_put_contents($fileName_ads, join("", $tpl_ads));
		}
	}

		
	echo "Indexes have been generated ....<br><br>";
	flush();	
}

function generatePages()
{
	global $targetPath;
	global $targetPath_ads;
	global $keywordsArr;
	global $keywords_num;
	global $publisherID;
	global $descriptionsArr;
	global $siteName;
    global $keywordsbag;
    global $keywordsbag_num;
    global $db;
	
	//echo "Generating Pages ....<br>";
	flush();	
	
	$aKeywords 		= array();
	$aKeywords_letter 	= array();
	$aKeywords_key 		= array();
	$aDesc			= array();
	$aDesc2			= array();
	$aDesc3			= array();
	$template		= preg_split('/\$\$(\w+)\$\$/', file_get_contents("generate_pages.tpl"), -1 , PREG_SPLIT_DELIM_CAPTURE );
	$template_ads		= preg_split('/\$\$(\w+)\$\$/', file_get_contents("generate_pages_ads.tpl"), -1 , PREG_SPLIT_DELIM_CAPTURE );
	
	
	//for($x = 0; $x < $keywords_num; $x++)
	for($x = 0; $x < $keywordsbag_num; $x++)
	{
		$row 			= &$keywordsbag[$x];
		$keyword		= $row["KeyWord"];
		
		$aKeywords[0]		= $keyword;
		$aKeywords_letter[0]	= $row["Letter"];
		$aKeywords_key[0]	= $row["Letter_Key"];
		
		
		for ($y = 1; $y < 5; $y++)
		{
			$x_ 			= rand(0, $keywords_num-1);
			// wait for 0.1 seconds  1000000 is one second
            //usleep(100000);
			$aKeywords[$y] 		= $keywordsArr[$x_]["KeyWord"];
			$aKeywords_letter[$y] 	= $keywordsArr[$x_]["Letter"];
			$aKeywords_key[$y] 	= $keywordsArr[$x_]["Letter_Key"];
		}
		
		$keywords = "";
		
		for ($y = 0; $y < 5; $y++)
		{
			$keywords .= ($y ? ",":"") . $aKeywords[$y];
			
		}
		
		$keywords .= ".";
		
		for($y = 0; $y < 7; $y++)
		{
			$aDesc[$y] 	= "";
			$x_ 		= rand(0, count($descriptionsArr)-1);
			$aDesc[$y] 	.= " " . $descriptionsArr[$x_][0];
			
		}
		
		for($y = 0; $y < 5; $y++)
		{
			$aDesc2[$y] 	= "";
			for($z = 0; $z < 3; $z++)
			{
				//$x_ 		= rand(0, count($descriptionsArr)-1);
				$x_ 		= rand(0, $keywords_num-1);
				//$aDesc2[$y] 	+= " <b><u>" . $aKeywords[$x_] . "</u></b>";
				$aDesc2[$y] 	.= " <b><u>" . $keywordsArr[$x_]["KeyWord"] . "</u></b>";
			}
			
		}
		
		for($y = 0; $y < 5; $y++)
		{
			$aDesc3[$y] 	= "";
			for($z = 0; $z < 7; $z++)
			{
				$x_ 		= rand(0, $keywords_num -1);
				$aDesc3[$y] 	.= " " . $keywordsArr[$x_]["KeyWord"];
			}
		}
		
		//$letter 	= trim($row["Letter_Key"]) != "0" ? $row["Letter"] . "_" . $row["Letter_Key"] : $row["Letter"];
		$letter 	= trim($row["Letter"]) ;
		$letterCode	= ord($letter);
		//$letter		= ($letterCode >= 65 && $letterCode <= 90) || ($letterCode >= 97 && $letterCode <= 122) ? strtoupper($letter) : "X";
		$letter = substr($letter,0,1) ;
		
		$fileName	    = "$targetPath/$letter/" . putDash($keyword) . ".htm";
		$fileName_ads	= "$targetPath_ads/$letter/" . putDash($keyword) . ".htm";
		
		
		switch(($x_ + 1) % 2)
		{
			case 0:
				$google_type1	= "image";
				$google_type2	= "text";
				$google_type3	= "image";				
				break;
				
			case 1:
				$google_type1	= "text";
				$google_type2	= "image";
				$google_type3	= "text";				
				break;				
		}
		
		$tpl	    = $template;
		$tpl_ads	= $template_ads;
		$loop1	= "";
		$loop2	= "";
		
		for($y = 0; $y < 5; $y++)
		{
			$loop1 .= "<tr>";
	                $loop1 .= "<td align=center valign=middle> <img src='../images/logo.gif' alt='$keyword' width=32 height=32/> <a name=\"" . putDash($aKeywords[$y]) . "\"></a> </td>\r\n";
	                $loop1 .= "<td width='90%' valign='top'>";
	                $loop1 .= "<h3>" . $aKeywords[$y] . "</h3> <p> " . trim($aDesc[$y]) . "</p>" . trim($aDesc2[$y]) . "<b><u> $keyword</u></b> " . trim($aDesc3[$y]) . "\r\n";
	                $loop1 .= "</td>\r\n";
	                $loop1 .= "</tr>\r\n";
		}
		
		for($y = 0; $y < 5; $y++)
		{
			//$letter 	= trim($aKeywords_key[$y]) != "0" ? $aKeywords_letter[$y] . "_" . $aKeywords_key[$y] : $aKeywords_letter[$y];
			$letter 	= trim($aKeywords_letter[$y]) ;
			$letterCode	= ord($letter);
			$letter = substr($letter,0,1);
            //$letter		= ($letterCode >= 65 && $letterCode <= 90) || ($letterCode >= 97 && $letterCode <= 122) ? strtolower($letter) : "X";
			$loop2		.= "<li><a href=\"../$letter/" . putDash($aKeywords[$y]) . ".htm\">" . $aKeywords[$y] . "</a></li>\r\n";
		}
		
		$values 		= array();
		$values["Keyword"]	= $keyword;
		$values["publisher_id"]	= $publisherID;
		$values["loop1"]	= $loop1;
		$values["loop2"]	= $loop2;
		$values["google_type1"]	= $google_type1;
		$values["google_type2"]	= $google_type2;
		$values["google_type3"]	= $google_type3;
	    $values["site_name"]	= $siteName;	
		
        fillTemplate($tpl, $values);
        fillTemplate($tpl_ads, $values);
        
	    file_put_contents($fileName, join("", $tpl));
	    file_put_contents($fileName_ads, join("", $tpl_ads));
	    
	    $stm  = "UPDATE keywords SET visited=1 WHERE KeyWord = '$keyword'" ;
		$db->Execute($stm);

		//if(($x+1) % ( $keywordsbag_num / 1000) == 0)
		{
			//echo round((($x+1) / $keywordsbag_num * 100)) . "%<br>";
			 //echo "$x \t";
			flush();
			//usleep(1000000);
		}
	}
	
	//echo "Pages have been generated ....<br>";
	flush();	
}

function fillTemplate(&$template, &$values)
{
	for($y = 1; $y < count($template); $y+=2)
	{
		$key = $template[$y];
		if(key_exists($key, $values))
			$template[$y] = $values[$key];
		else
			$template[$y] = "";
	}
}


 function traverseDir($dir)
 {

  global $main_keyword;
  global $keywordsArr;
  global $keywords_num;
  global $publisherID;
  global $siteName;
  global $siteName1;
  global $siteName2;


  $str = "" ;
  		
  if(!($dp = opendir($dir))) die("Cannot open $dir.");


    while((false !== $file = readdir($dp)))
     {
       if(!is_dir("$dir/$file"))
         {
            $x_ 		= rand(0, $keywords_num -1);
            $row 	    = &$keywordsArr[$x_];
            $keyword 	= $row["KeyWord"];

            $str .= "<a href='$main_keyword/".$file."'>".$keyword."</a><BR>";
           
         }
      }
     closedir($dp);

     $fileName ="../../index.html";
     //$fileName2 ="../index.htm";
     //$f = fopen($fileName2, "w");
     //fwrite($f, "Hello");
     //fclose($f);

     $values 		= array();
     $values["newstr2"]	= $str;
     $values["main_keyword"] = $main_keyword;
     $values["publisher_id"] = $publisherID;
     $values["site_name"]	= $siteName;
     $values["site_name1"]	= $siteName1;
     $values["site_name2"]	= $siteName2;


     $template		= preg_split('/\$\$(\w+)\$\$/', file_get_contents("index.tpl"), -1 , PREG_SPLIT_DELIM_CAPTURE );
     $tpl	= $template;
     fillTemplate($tpl, $values);
	 file_put_contents($fileName, join("", $tpl));
	 		
  }


function random_row($table, $column) {

      $max_sql = "SELECT max(" . $column . ")

                  AS max_id

                  FROM " . $table;

      $max_row = mysql_fetch_array(mysql_query($max_sql));

      $random_number = mt_rand(1, $max_row['max_id']);

      $random_sql = "SELECT * FROM " . $table . "

                     WHERE " . $column . " >= " . $random_number . "

                     ORDER BY " . $column . " ASC

                     LIMIT 1";

      $random_row = mysql_fetch_row(mysql_query($random_sql));

      if (!is_array($random_row)) {

          $random_sql = "SELECT * FROM " . $table . "

                         WHERE " . $column . " < " . $random_number . "

                         ORDER BY " . $column . " DESC

                         LIMIT 1";

          $random_row = mysql_fetch_row(mysql_query($random_sql));

      }

      return $random_row;

  }


?>
