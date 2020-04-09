<?php


$tableName 			= 	"es3cloud";	
$userField	  		=	"user";	
$filenameField 		= 	"filename";	
	include_once "ES3Variables.php";

	try
{
	$db = new PDO("mysql:host=$db_host;dbname=$db_name", $db_user, $db_password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
}
catch(PDOException $e)
{
	Error("Could not connect to database.", $e->getMessage(), 501);
}


$id = isset($_GET["id"])? $_GET["id"] : "";

	$stmt = $db->prepare("SELECT * FROM $tableName WHERE $filenameField = ?");
	$stmt->bindParam(1, $id);
	$stmt->execute();
	$row = $stmt->fetch();

	header('Content-Type:'.$row['mime']);
	echo $row['data']; 
	// foreach($rows as $row)
	// {

	// 	echo "<li><a target='_blank' href='view.php?id=".$row[$filenameField] . "</li>";
	// }
