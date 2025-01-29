import Button from "@mui/material/Button";

const DeleteModal = ({ onExit, film }) => {
  const handleDelete = () => {
    console.log("Deleting film:", film);
    // Виклик DELETE
    onExit();
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h2>Are you sure you want to delete {film?.title}?</h2>
        <div>
          <Button onClick={handleDelete} color="error">Yes, Delete</Button>
          <Button onClick={onExit}>Cancel</Button>
        </div>
      </div>
    </div>
  );
};

export default DeleteModal;