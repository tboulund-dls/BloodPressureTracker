## Start our project :rocket:

short explanation on how to run our project inside docker

the first step is to create our Jenkins image from our self made dockerfile

```
docker build -f DockerFile.Jenkins -t my-jenkins-with-docker .
```

than you con run docker

```
docker compose up
```

## short explanation of featureHub values

> [!NOTE]
> her we checking for the measurement service:
>
> \_isFeatureMeasurementEnabled = fh["m_service"].IsEnabled
>
> and her we check for the patient service:
>
> \_isFeaturePatientEnabled = fh["p_service"].IsEnabled
>
> the last one checks if the location lock is active:
>
> \_isFeatureDkEnabled = fh["o_dk"].IsEnabled

> [!IMPORTANT]
> **in the FeatureHub project in featureService at line 24 you have to put your API key**
