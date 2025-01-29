import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
const Table = ({ onModelOpen, onDeleteFilm }) => {
  const [films, setFilms] = useState([]);

  useEffect(() => {
    const getFilms = async () => {
      try {
        const result = await fetch("https://localhost:7091/films");
        const data = await result.json();


        setFilms(data);
      } catch (error) {
        console.error("Error fetching films:", error);
      } finally {
        console.log("Films fetched");
      }
    };

    getFilms();
  }, []);

  return (
    <>
      <Button color="secondary" onClick={() => onModelOpen(null, "add")}>
        <AddIcon />
      </Button>

      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Genre</th>
            <th>Director</th>
            <th>Release</th>
            <th>Rating</th>
            <th>Descretion</th>
            <th>Option</th>
          </tr>

          {films.map((film) => {
            return (
              <tr key={film.id}>
                <td>{film.title}</td>
                <td>{film.genre}</td>
                <td>{film.director}</td>
                <td>{film.release}</td>
                <td>{film.rating}</td>
                <td>{film.description}</td>
                    <td>
                    <Button
                    variant="outlined"
                    size="small"
                    color="error"
                    startIcon={<EditIcon />}
                    onClick={() => onModelOpen(film, "edit")}
                  />
                
                </td>
                <td>
                  <Button
                    variant="outlined"
                    size="small"
                    startIcon={<DeleteIcon />}
                    onClick={() => onDeleteFilm(film)}
                  />
                </td>
              </tr>
            );
          })}
        </thead>
      </table>
    </>
  );
};

export default Table;
