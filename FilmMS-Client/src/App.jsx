import Table from "./compontents/table/table";
import Modal from "./compontents/modal/modal";
import { useState } from "react";

function App() {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [modalData, setFilms] = useState([]);
  const [method, setMethod] = useState("");

  const showModal = () => {
    setIsModalVisible(true);
  };
  const hideModal = () => {
    setIsModalVisible(false);
  };

  const openModalWithFilm = (film, methodType) => {
    setFilms(film);
    setMethod(methodType);
    showModal();
    console.log(film);
  };

  return (
    <>
      <Table onModelOpen={openModalWithFilm} />
      {isModalVisible && <Modal onExit={hideModal} method={method} />}
    </>
  );
}

export default App;
