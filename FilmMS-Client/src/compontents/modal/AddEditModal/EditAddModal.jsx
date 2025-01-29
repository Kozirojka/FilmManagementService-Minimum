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
    let { name, value } = e.target;

    if (name === "id") {
      value = value ? parseInt(value, 10) : 0;
    } else if (name === "rating") {
      value = value ? parseFloat(value) : 0;
    }

    setFilmState({
      ...filmState,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    let url = "https://localhost:7091/films/";
    let options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: "",
    };

    if (method === "edit") {
      url = `${url}${filmState.id}`;
      options.method = "PUT";
      options.body = JSON.stringify(filmState);
    } else {

      const { id, ...filmData } = filmState; // eslint-disable-line no-unused-vars

      options.body = JSON.stringify(filmData);
    }

    try {
      const response = await fetch(url, options);
      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const data = await response.json();
      console.log("Server response:", data);
    } catch (error) {
      console.error("Error submitting data:", error);
    }
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
              type="date"
              value={
                filmState.releaseDate
                  ? new Date(filmState.releaseDate).toISOString().split("T")[0]
                  : ""
              }
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
            <label htmlFor="rating">Rating: </label>
            <input
              id="rating"
              name="rating"
              value={filmState.rating}
              placeholder="rating"
              onChange={handleChange}
            />
          </div>
        </form>

        <IconButton type="submit" onClick={handleSubmit}>
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
