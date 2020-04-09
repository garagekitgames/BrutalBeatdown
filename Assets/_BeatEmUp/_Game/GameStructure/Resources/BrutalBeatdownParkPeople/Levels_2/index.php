<form method="POST" action="ES3Cloud.php" enctype="multipart/form-data">
	<input type="text" name="apiKey">
    <input type="file" name="file">
    <input type="hidden" name="postFile">
    <input type="submit" value="postFile">
</form>
<p>
	
</p>
<ol>
	
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



	$stmt = $db->prepare("SELECT $filenameField FROM $tableName ORDER BY LENGTH($filenameField), $filenameField");
	//$stmt->bindParam(":user", GetPOSTUser());
	$stmt->execute();
	$rows = $stmt->fetchAll();
	foreach($rows as $row)
	{

		echo "<li><a target='_blank' href='view.php?id=".$row[$filenameField] . "'>".$row[$filenameField] ."</a> </li>";
	}



function GetPOSTUser()
{
	return isset($_POST["user"]) ? $_POST["user"] : "";
}
	?>

</ol>
<?php

// This will return all files in that folder
$files = scandir("uploads");

// If you are using windows, first 2 indexes are "." and "..",
// if you are using Mac, you may need to start the loop from 3,
// because the 3rd index in Mac is ".DS_Store" (auto-generated file by Mac)
for ($a = 2; $a < count($files); $a++)
{
    ?>
    <p>
    	<!-- Displaying file name !-->
        <?php echo $files[$a]; ?>

        <!-- href should be complete file path !-->
        <!-- download attribute should be the name after it downloads !-->
        <a href="uploads/<?php echo $files[$a]; ?>" download="<?php echo $files[$a]; ?>">
            Download
        </a>
        <a href="delete.php?name=uploads/<?php echo $files[$a]; ?>" style="color: red;">
    Delete
</a>
    </p>
    <?php
}