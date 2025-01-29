import Button from "@mui/material/Button";

const DeleteModal = ({ onExit, film, onConfirm }) => {
  
  const handleDelete = async () => {
    try {
      await onConfirm(); 
      onExit();
    } catch (error) {
      console.error("Delete failed:", error);
    }
  };

  return (
    <div className="modal-overlay">
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