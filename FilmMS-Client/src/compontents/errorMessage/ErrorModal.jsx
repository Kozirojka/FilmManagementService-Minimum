import "./ErrorModal.css";
import Button from "@mui/material/Button";
 
const ErrorModal = ({ errors, onClose }) => {
  if (!errors || errors.length === 0) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h3>Error</h3>
        <ul>
          {errors.map((err, index) => (
            <li key={index}>{err}</li>
          ))}
        </ul>
        <Button
          variant="outlined"
          size="small"
          color="error"
          onClick={onClose}  
        >
            Close
        </Button>
      </div>
    </div>
  );
};

export default ErrorModal;
