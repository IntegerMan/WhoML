# WhoML
Data Science experiments centered around Doctor Who datasets by [Matt Eland](https://MattEland.dev)

Doctor Who is Copyright © British Broadcasting Corporation (BBC) 1963, 2022.
No infringement of this copyright is either implied or intended.

## Data Sources

Data taken from [Jeanmidev's Doctor Who dataset on Kaggle](https://www.kaggle.com/jeanmidev/doctor-who) includes data from the IMDB dataset as well as the Doctor Who Guide website.

*Note: Script details are also available, but were not chosen for this experiment and are excluded to cut down on extra storage needs*

## Flow

Values are first loaded in [the aggregation notebook](/EDA/WhoAggregation.ipynb) from multiple data files. The resulting dataset is saved as `Merged.csv`.

The [feature extraction notebook](/EDA/WhoFeatureExtraction.ipynb) then extracts meaningful features from that data and saves it into a `Processed.csv` file that can be used for visualization or machine learning.

