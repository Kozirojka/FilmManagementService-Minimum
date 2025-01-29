import Table from "./compontents/table/table";
import Modal from "./compontents/modal/AddEditModal/EditAddModal";
import DeleteModal from "./compontents/modal/DeleteModal/DeleteModal";

import { useState } from "react";

function App() {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);

  const [modalFilm, setFilms] = useState([]);
  const [method, setMethod] = useState("");

  const showModal = () => {
    setIsModalVisible(true);
  };
  const hideModal = () => {
    setIsModalVisible(false);
    setFilms([]);
  };

  const showDeleteModal = () => {
    setIsDeleteModalVisible(true);
  };

  const hideDeleteModal = () => {
    setIsDeleteModalVisible(false);
    setFilms([]);
  };

  const openDeleteWindow = (film) => {
    setFilms(film);
    showDeleteModal();
  };

  const openModalWithFilm = (film, methodType) => {
    setFilms(film);
    setMethod(methodType);
    showModal();
  };

  return (
    <>
      <Table onModelOpen={openModalWithFilm} onDeleteFilm={openDeleteWindow} />
      {isModalVisible && (
        <Modal onExit={hideModal} method={method} film={modalFilm} />
      )}
      {isDeleteModalVisible && (
        <DeleteModal onExit={hideDeleteModal} film={modalFilm} />
      )}
    </>
  );
}

export default App;
