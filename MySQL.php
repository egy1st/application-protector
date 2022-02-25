<?php
class MySQL
{
	public $link;

	public function __construct($server, $user, $pass, $database=null, $persistant = true)
	{
		if($persistant)
			$this->link = mysql_pconnect ($server, $user, $pass);
		else
			$this->link = mysql_connect ($server, $user, $pass, true);

		$this->SelectDB($database);
	}

	public function SelectDB($database)
	{
		if($database)
		 {
			mysql_select_db ($database, $this->link);
		 }
	}

	public function Execute($stmt)
	{
		return mysql_query($stmt, $this->link);
	}

	public function FetchAll($stmt, $justFirstColumn = false, $associative = false)
	{
		$ret = array();
		$result = mysql_unbuffered_query($stmt);
		
		if($justFirstColumn)
		{
			while($row = mysql_fetch_row($result))
				$ret [] = $row[0];
		}
		else
		{
			if($associative)
			{
				while($row = mysql_fetch_assoc($result))
					$ret [] = $row;
			}
			else
			{
				while($row = mysql_fetch_row($result))
					$ret [] = $row;
			}
		}

		return $ret;
	}

	public function FirstRow($stmt, $associative = false)
	{
		$result = mysql_query($stmt);
		
		if($associative)
			return mysql_fetch_assoc($result);
		else
			return mysql_fetch_array($result, MYSQL_NUM);
	}

	public function FirstValue($stmt)
	{
		$row = $this->FirstRow($stmt);
		return $row[0];
	}



	public function ResultSet($stmt, $associative = false)
	{
		return new MySQLResultSet(mysql_query($stmt), $associative);
	}


	public function Exists($stmt)
	{
		$result = mysql_query($stmt);

		if(mysql_num_rows($result) > 0)
			return true;
		else
			return false;
	}

	public function SQLAssociativeArrayByVals($stmt)
	{
		$result = mysql_query($stmt);

		$fields = @mysql_num_fields($result);

		$row = null;
		$ret = array();


		//questionid, $statementid, value;

		$tmpArr = &$ret;


		while($row = @mysql_fetch_row($result))
		{

			$tmpArr = &$ret;

			//$tmpArr = array();

			for($x = 0; $x < $fields - 2; $x++)
			{
				if(!array_key_exists($row[$x], $tmpArr))
				{
					$tmpArr[$row[$x]] = array();
				}
				$tmpArr = &$tmpArr[$row[$x]];
			}
			
			$key = $row[$x];
			$tmpArr[$key][] = $row[$x+1];
		}

		return $ret;
	}

	public function AssociativeArrayByVals($stmt, $dimensions, $valsAsArray = true)
	{
		$result = mysql_query($stmt);

		$fields = @mysql_num_fields($result);

		$row = null;
		$ret = array();


		//questionid, $statementid, value;

		$tmpArr = &$ret;


		while($row = @mysql_fetch_row($result))
		{

			$tmpArr = &$ret;

			//$tmpArr = array();

			for($x = 0; $x < $dimensions-1; $x++)
			{
				if(!array_key_exists($row[$x], $tmpArr))
				{
					$tmpArr[$row[$x]] = array();
				}
				$tmpArr = &$tmpArr[$row[$x]];
			}

			$key = $row[$x];

			if(($fields - $dimensions) > 1 || $valsAsArray)
			{
				$vals = array();

				for($x = $dimensions; $x < $fields; $x++)
				{
					$vals[] = $row[$x];
				}

			}
			else
			{
				$vals = $row[$dimensions];
			}
			

			$tmpArr[$key] = $vals;
		}

		return $ret;
	}

	public function PreparePivotSTMT($tableName, $rowField, $colField, $valField, $whereClause, $colsValues, $aggregate, $includeTotal = false)
	{
		$pivotCols = "";

		foreach($colsValues as $value)
		{
			$pivotCols .= "$aggregate(IF($colField = '$value', $valField, null)) AS '$colField$value', ";
		}

		$pivotCols = substr($pivotCols, 0, strlen($pivotCols)-2);
		


		$stmt =		"	SELECT $rowField, $pivotCols
						FROM $tableName
						$whereClause
						GROUP BY $rowField
					";


		if($includeTotal)
		{
			$stmt .=	"	UNION ALL
							SELECT 'total', $pivotCols
							FROM $tableName
							$whereClause
						";
		}

		return $stmt;
	}
}

class MySQLResultSet
{
	public	$recordCount	= 0;
	public	$fieldsCount	= 0;
	public	$rowIndex		= 0;
	private $result			= null;
	private $associative	= false;


	public function  __construct($result, $associative = false)
	{
		$this->result = $result;
		$this->recordCount = mysql_num_rows($result);
		$this->fieldsCount = mysql_num_fields($result);
		$this->associative = $associative;
	}


	public function NextRow()
	{
		if($this->associative)
			return mysql_fetch_assoc($this->result);
		else
			return mysql_fetch_array($this->result, MYSQL_NUM);
	}
}
?>
