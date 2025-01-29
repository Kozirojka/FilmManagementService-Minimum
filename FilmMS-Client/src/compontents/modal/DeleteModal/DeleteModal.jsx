import Button from "@mui/material/Button";

const DeleteModal = ({ onExit, film }) => {
  
  const handleDelete = async () => {
    console.log("Deleting film:", film);
    
    const response = await fetch(`https://localhost:7091/films/${film.id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
    });

    const data = await response.json();

    console.log(data);
    
    onExit();
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h3>Are you sure you want to delete {film?.title}?</h3>
        <div>
          <Button onClick={handleDelete} variant="outlined" color="error">Yes, Delete</Button>
          <Button onClick={onExit} >Cancel</Button>
        </div>
      </div>
    </div>
  );
};

export default DeleteModal;