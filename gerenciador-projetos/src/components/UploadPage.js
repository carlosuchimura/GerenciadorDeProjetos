import React, { useState } from 'react';
import axios from 'axios';

const UploadPage = () => {
  const [files, setFiles] = useState([]);

  const handleFileInputChange = (e) => {
    const newFiles = [...e.target.files];
    setFiles([...files, ...newFiles]);
  };

  const handleRemoveFile = (index) => {
    const newFiles = [...files];
    newFiles.splice(index, 1);
    setFiles(newFiles);
  };

  const handleSubmit = async () => {
    const formData = new FormData();
    files.forEach(file => {
      formData.append('files', file);
    });

    try {
      const response = await axios.post('https://localhost:44341/api/projetos/upload', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      console.log('Arquivos enviados com sucesso:', response.data);
      // Limpar os arquivos após o envio
      setFiles([]);
    } catch (error) {
      console.error('Erro ao enviar arquivos:', error);
    }
  };

  return (
    <div>
      <h1>Upload de Arquivos</h1>
      <div>
        <input
          type="file"
          id="fileInput"
          style={{ display: 'none' }}
          multiple
          onChange={handleFileInputChange}
        />
        <button onClick={() => document.getElementById('fileInput').click()}>Adicionar Arquivos</button>
      </div>
      {files.length > 0 && (
        <div>
          <h2>Arquivos Selecionados:</h2>
          <ul>
            {files.map((file, index) => (
              <li key={index}>
                {file.name}
                <button onClick={() => handleRemoveFile(index)}>Excluir</button>
              </li>
            ))}
          </ul>
          <button onClick={handleSubmit}>Enviar Arquivos</button>
        </div>
      )}
    </div>
  );
};

export default UploadPage;
