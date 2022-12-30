﻿using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;

namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers;

public interface IObjectRelationalMapper
{
    IObjectRelationalMapper RegisterDependency<TAbstract, TConcrete>() where TConcrete : TAbstract;

    IObjectRelationalMapper AddParameter<T>(string name, T value);

    int ExecuteNonQuery(string query, QueryExecutionSettings? executionSettings);

    IEnumerable<int> ExecuteNonQueries(IEnumerable<string> queries, QueryExecutionSettings? executionSettings);

    IEnumerable<T> ExecuteQuery<T>(string query, QueryExecutionSettings? executionSettings, MappingSettings? mappingSettings);

    T ExecuteScalar<T>(string query, QueryExecutionSettings? executionSettings);
}