import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import EditIcon from "@mui/icons-material/Edit";

const Table = ({onModelOpen}) => {

  const [films, setFilms] = useState([]);

  useEffect(() => {
    const getFilms = async () => {
      try {
        const result = await fetch("https://localhost:7091/films");
        const data = await result.json();

        console.log(data)

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
      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Genre</th>
            <th>Director</th>
            <th>Release</th>
            <th>Rating</th>
            <th>Descretion</th>
          </tr>

          {films.map((film) => {
            return (
              <tr key={film.id}>
                <td>{film.title}</td>
                <td>{film.genre}</td>
                <td>{film.director}</td>
                <td>{film.release}</td>
                <td>{film.rating}</td>
                <td>{film.descretion}</td>
                <td>
                  <Button
                    variant="outlined"
                    size="small"
                    sx={{
                      borderColor: "red", 
                      color: "red",
                      "&:hover": { borderColor: "darkred", color: "darkred" },
                    }}
                    onClick={() => onModelOpen(film, "edit")}
                  >
                    <EditIcon />
                  </Button>
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
