import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ProjectList = () => {
  const [projects, setProjects] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:44341/api/projetos')
      .then(response => {
        setProjects(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <div>
      <h1>Lista de Projetos</h1>
      <div className="project-grid">
        {projects.map(project => (
          <div key={project.id} className="project-card">
            <h2>{project.nome}</h2>
            <p>Área: {project.area}</p>
            <p>Data de Início: {project.dataInicio}</p>
            <p>Data de Fim: {project.dataFim || 'N/A'}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProjectList;