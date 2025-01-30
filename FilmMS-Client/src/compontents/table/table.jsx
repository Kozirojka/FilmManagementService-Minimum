import { useState } from "react";
import Button from "@mui/material/Button";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import "./table.css";

const Table = ({
  data,
  onModelOpen,
  onDeleteFilm,
  searchValue,
  onSearchChange,
}) => {
  const [page, setPage] = useState(0);
  const pageSize = 5;

  const handlePrevious = () => {
    setPage((prevPage) => prevPage - 1);
  };

  const handleNext = () => {
    setPage((prevPage) => prevPage + 1);
  };

  const startIndex = page * pageSize;
  const endIndex = startIndex + pageSize;
  const paginatedFilms = data.slice(startIndex, endIndex);

  return (
    <div className="table-container">
      <div>
        <Button color="secondary" onClick={() => onModelOpen(null, "add")}>
          <AddIcon />
        </Button>

        <input
          type="text"
          value={searchValue}
          placeholder="Title or director or ID"
          onChange={(e) => onSearchChange(e.target.value)}
        />
      </div>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>Genre</th>
            <th>Director</th>
            <th>Release</th>
            <th>Rating</th>
            <th>Description</th>
            <th>Option</th>
          </tr>
        </thead>
        <tbody>
          {paginatedFilms.map((film) => {
            return (
              <tr key={film.id}>
                <td>{film.id}</td>
                <td>{film.title}</td>
                <td>{film.genre}</td>
                <td>{film.director}</td>
                <td>
                  {new Date(film.releaseDate).toISOString().split("T")[0]}
                </td>
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
        </tbody>
      </table>

      <div className="paginator">
        <Button
          onClick={handlePrevious}
          disabled={page === 0}
          startIcon={<ArrowBackIcon />}
        />
        <Button
          onClick={handleNext}
          disabled={data.length < endIndex}
          startIcon={<ArrowForwardIcon />}
        />
      </div>
    </div>
  );
};

export default Table;
