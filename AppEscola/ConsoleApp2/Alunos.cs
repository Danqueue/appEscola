﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Curso { get; set; }
    public DateTime DataMatricula { get; set; }
}