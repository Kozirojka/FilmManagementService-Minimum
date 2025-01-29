import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import CheckIcon from "@mui/icons-material/Check";

import { useState } from "react";

const EditAddModal = ({ onExit, method, film }) => {
  const [filmState, setFilmState] = useState(
    film || {
      id: "",
      title: "",
      genre: "",
      director: "",
      releaseDate: "",
      description: "",
      rating: "",
    }
  );

  const handleChange = (e) => {
    setFilmState({
      ...filmState,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault(); 

    if (method === "edit") {
        console.log("Editing data:", filmState);
    }
    
    if(method === "add"){
        console.log("Adding data:", filmState);
    }
    console.log("Submitting data:", filmState);

  };


  return (
    <div className="modal">
      <div className="modal-content">
        {method === "edit" ? <h2>Edit Film</h2> : <h2>Add Film</h2>}

        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="title">Title: </label>
            <input
              id="title"
              name="title"
              value={filmState.title}
              placeholder="title"
              onChange={handleChange}
            />
          </div>

          <div>
            <label htmlFor="genre">Genre: </label>
            <input
              id="genre"
              name="genre"
              value={filmState.genre}
              placeholder="genre"
              onChange={handleChange}
            />
          </div>

          <div>
            <label htmlFor="director">Director: </label>
            <input
              id="director"
              name="director"
              value={filmState.director}
              placeholder="director"
              onChange={handleChange}
            />
          </div>

          <div>
            <label htmlFor="releaseDate">Release Date: </label>
            <input
              id="releaseDate"
              name="releaseDate"
              value={filmState.releaseDate}
              placeholder="release date"
              onChange={handleChange}
            />
          </div>

          <div>
            <label htmlFor="description">Description:</label>
            <input
              id="description"
              name="description"
              value={filmState.description}
              placeholder="description"
              onChange={handleChange}
            />
          </div>

          <div>
            <label htmlFor="raring">Rating: </label>
            <input
              id="rating"
              name="rating"
              value={filmState.rating}
              placeholder="rating"
              onChange={handleChange}
            />
          </div>

       

        </form>

        <IconButton type="submit">
          <CheckIcon />
        </IconButton>

        <IconButton onClick={onExit}>
          <CloseIcon />
        </IconButton>
      </div>
    </div>
  );
};

export default EditAddModal;
