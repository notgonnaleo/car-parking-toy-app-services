﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VeiculosFagron.Repository;

namespace VeiculosFagron.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeloController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<ModeloController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly IModeloRepository _modeloRepository;

        // Declarando e armazenando as configurações de dependências
        public ModeloController(IConfiguration config, ILogger<ModeloController> logger, IModeloRepository modeloRepository)
        {
            _config = config;
            _logger = logger;
            _modeloRepository = modeloRepository;
        }

        #endregion

        #region Modelos

        [HttpGet]
        [Route("getModelos")]
        public async Task<ActionResult<List<Modelo>>> getModelos()
        {
            try
            {
                var data = await _modeloRepository.GetModelos();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetModelos: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        [Route("getModelo/{id_modelo}")]
        public async Task<ActionResult<Modelo>> getModelo(int id_modelo)
        {
            try
            {
                var data = await _modeloRepository.GetModelo(id_modelo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetModelo: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpPost]
        [Route("createModelo")]
        public async Task<ActionResult<bool>> createModelo(Modelo model)
        {
            try
            {
                var data = await _modeloRepository.CreateModelo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateModelo: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpPut]
        [Route("updateModelo")]
        public async Task<ActionResult<bool>> updateModelo(Modelo model)
        {
            try
            {
                var data = await _modeloRepository.UpdateModelo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateModelo: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpDelete]
        [Route("deleteModelo")]
        public async Task<ActionResult<bool>> deleteModelo(Modelo model)
        {
            try
            {
                var data = await _modeloRepository.DeleteModelo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteModelo: Erro na requisição dos dados");
                return false;
            }

        }

        #endregion

    }
}
