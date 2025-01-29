import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";

const Modal = ({ onExit, method }) => {
    return (
        <div className="modal">
        <div className="modal-content">

            {console.log(method)}
            {method === 'edit' ? (
                <h2>Edit Film</h2>
            ) : (
                <h2>Add Film</h2>
            )}
            <IconButton onClick={onExit}>
                <CloseIcon />
            </IconButton>
        </div>
        </div>
    );
}

export default Modal;